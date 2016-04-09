using System;
using System.Collections.Generic;

namespace Pw.Eiti.Pain.Wzielin3.Lab2
{
    internal class DataAccessLayer
    {
        internal static ApplicationModel GetApplicationModel()
        {
            return new ApplicationModel()
            {
                Models = new List<PointModel>
                {
                    new PointModel
                    {
                        Color = ColorType.Red,
                        X = 30,
                        Y = 20,
                        Label = "Punkty pierwszy"
                    },
                    new PointModel
                    {
                        Color = ColorType.Blue,
                        X = -20,
                        Y = 100,
                        Label = "Drugi punkt"
                    },
                    new PointModel
                    {
                        Color = ColorType.Red,
                        X = 200,
                        Y = -50,
                        Label = "Trzeci punkt"
                    },
                    new PointModel
                    {
                        Color = ColorType.Green,
                        X = -500,
                        Y = -30,
                        Label = "Ostatni punkt"
                    }
                }
            };
        }
    }
}