using OSGeo.OGR;
using System.IO;

namespace conversor_Shapefile_to_GeoJSON.Control
{
    class Conversion : IControl
    {
        public void Converting(string source, string destination)
        {
            GdalConfiguration.ConfigureGdal();
            GdalConfiguration.ConfigureOgr();
            OSGeo.OGR.Ogr.RegisterAll();
            OSGeo.OGR.Driver drv = Ogr.GetDriverByName("ESRI Shapefile");
            DataSource ds = drv.Open(source, 0);

            OSGeo.OGR.Layer layer = ds.GetLayerByIndex(0);
            OSGeo.OGR.Feature f;
            layer.ResetReading();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine("{\"type\":\"FeatureCollection\", \"features\":[");
            while ((f = layer.GetNextFeature()) != null)
            {
                //Geometry
                var geom = f.GetGeometryRef();
                if (geom != null)
                {
                    var geometryJson = geom.ExportToJson(new string[] { });
                    sb.Append("{\"type\":\"Feature\",\"geometry\":" + geometryJson + ",");
                }

                //Properties
                int count = f.GetFieldCount();
                if (count != 0)
                {
                    sb.Append("\"properties\":{");
                    for (int i = 0; i < count; i++)
                    {
                        FieldType type = f.GetFieldType(i);
                        string key = f.GetFieldDefnRef(i).GetName();

                        if (type == FieldType.OFTInteger)
                        {
                            var field = f.GetFieldAsInteger(i);
                            sb.Append("\"" + key + "\":" + field + ",");
                        }
                        else if (type == FieldType.OFTReal)
                        {
                            var field = f.GetFieldAsDouble(i);
                            sb.Append("\"" + key + "\":" + field + ",");
                        }
                        else
                        {
                            var field = f.GetFieldAsString(i);
                            sb.Append("\"" + key + "\":\"" + field + "\",");
                        }

                    }
                    sb.Length--;
                    sb.Append("},");
                }

                //FID
                long id = f.GetFID();
                sb.AppendLine("\"id\":" + id + "},");
            }

            sb.Length -= 3;
            sb.AppendLine("");
            sb.Append("]}");
            File.AppendAllText(destination, sb.ToString());
        }
    }
}
