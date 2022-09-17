using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Dyn3d = Autodesk.DesignScript.Geometry;
using TS3d = Tekla.Structures.Geometry3d;

namespace TeklaLink
{
    public class ModelObjectBuilder
    {
        public static void InsertBeamsByPoints(List<Dyn3d.Point> points)
        {
            Model model = new Model();
            if (model.GetConnectionStatus())
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    Dyn3d.Point pt1 = points[i];
                    Dyn3d.Point pt2 = points[i + 1];
                    TS3d.Point startPoint = new TS3d.Point(pt1.X, pt1.Y, pt1.Z);
                    TS3d.Point endPoint = new TS3d.Point(pt2.X, pt2.Y, pt2.Z);
                    Beam b = new Beam(startPoint, endPoint)
                    {
                        Profile = new Profile { ProfileString = "PL100*10" },
                        Material = new Material { MaterialString = "Steel_Undefined" },
                        Class = "3",
                        Name = "dynamo-test",
                        Position = new Position
                        {
                            Plane = Position.PlaneEnum.MIDDLE,
                            Depth = Position.DepthEnum.MIDDLE
                        },
                    };
                    b.Insert();
                }
                model.CommitChanges();
            }
        }
    }
}
