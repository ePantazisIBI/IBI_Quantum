using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace IBI_toolkit
{
    internal class ConvexHull
    {

        internal static Polyline CalculateCH(List<Point3d> inputPoints)
        {

            //find the lower and the rightmost point take the x,y
            FindLowest(inputPoints);

            //sort the points based on the angle they are maxing with the positive X axis
            AngleSort(inputPoints);

            //call Grahams sort algortihm in order to create the conver hull
            List<Point3d> CHpts = GrahamSort(inputPoints);

            return new Polyline(CHpts);

        }


        private static List<Point3d> GrahamSort(List<Point3d> inputPoints)
        {

            List<Point3d> CH = new List<Point3d>();

            CH.Add(inputPoints[0]);
            CH.Add(inputPoints[1]);

            int i = 2;

            while (i < inputPoints.Count)
            {
                double det = Determinant(CH[CH.Count - 2],
                                           CH[CH.Count - 1],
                                           inputPoints[i]
                                           );
                if (det > -0.000001) //check for left turn
                {
                    CH.Add(inputPoints[i]);
                    i++;

                }
                else if (det < 0 && CH.Count > 2)
                {
                    CH.RemoveAt(CH.Count - 1);
                }
            }

            CH.Add(CH[0]);

            return CH;
        }

        private static double Determinant(Point3d pt1, Point3d pt2, Point3d pt3)
        {
            double[] row1 = new double[3];
            double[] row2 = new double[3];
            double[] row3 = new double[3];
            double det;

            row1[0] = row2[0] = row3[0] = 1;

            row1[1] = pt1.X;
            row1[2] = pt1.Y;

            row2[1] = pt2.X;
            row2[2] = pt2.Y;

            row3[1] = pt3.X;
            row3[2] = pt3.Y;


            /*
             *       1 X1 Y1
             *  Det  1 X2 Y2
             *       1 X3 Y3
             * 
            */
            det = (row1[0] * row2[1] * row3[2]) +
                  (row1[1] * row2[2] * row3[0]) +
                  (row1[2] * row2[0] * row3[1]) -
                  (row1[0] * row2[2] * row3[1]) -
                  (row1[1] * row2[0] * row3[2]) -
                  (row1[2] * row2[1] * row3[0]);

            return det;
        }


        private static void FindLowest(List<Point3d> inputPoints)
        {
            int i;
            int m = 0; // index in the list of the lowest point

            for (i = 1; i < inputPoints.Count; i++)
            {
                if (inputPoints[i].Y < inputPoints[m].Y ||
                    inputPoints[i].Y == inputPoints[m].Y && inputPoints[i].X > inputPoints[m].X)
                {
                    m = i;
                }
                //manual swap in an array
                //create a temp point
                Point3d swapTemp = inputPoints[0];
                //pass the value that you have stored on 
                inputPoints[0] = inputPoints[m];
                //put the value that you stored in 
                inputPoints[m] = swapTemp;


            }
        }

        private static void AngleSort(List<Point3d> inputPoints)
        {
            int i, j;

            for (i = 1; i < inputPoints.Count - 1; i++)
            {
                for (j = i + 1; j < inputPoints.Count; j++)
                {
                    double angleI = Vector3d.VectorAngle(Vector3d.XAxis, new Vector3d(inputPoints[i] - inputPoints[0]));
                    double angleJ = Vector3d.VectorAngle(Vector3d.XAxis, new Vector3d(inputPoints[j] - inputPoints[0]));

                    if (angleJ < angleI)
                    {
                        //manual swap in an array
                        //create a temp point
                        Point3d swapTemp = inputPoints[i];
                        //pass the value that you have stored on 
                        inputPoints[i] = inputPoints[j];
                        //put the value that you stored in 
                        inputPoints[j] = swapTemp;

                    }
                    else if (Math.Abs(angleI - angleJ) < 0.00001) //do not do angleI= angleJ because they might be infinieismally 
                    {
                        double lengthI = (inputPoints[i] - inputPoints[0]).Length;
                        double lengthJ = (inputPoints[j] - inputPoints[0]).Length;

                        if (lengthJ < lengthI)
                        {
                            //manual swap in an array
                            Point3d swapTemp = inputPoints[i];
                            inputPoints[i] = inputPoints[j];
                            inputPoints[j] = swapTemp;
                        }

                    }
                }
            }

        }

    }
}