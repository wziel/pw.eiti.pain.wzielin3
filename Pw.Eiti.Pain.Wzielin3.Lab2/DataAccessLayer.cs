using System;
using System.Collections.Generic;

namespace Pw.Eiti.Pain.Wzielin3.Lab2
{
    internal class DataAccessLayer
    {
        internal static ApplicationModel GetApplicationModel()
        {
            var appModel = new ApplicationModel();
            appModel.Add(new PointModel(appModel)
            {
                Color = ColorType.Red,
                X = 30,
                Y = 20,
                Label = "Punkty pierwszy"
            });
            appModel.Add(new PointModel(appModel)
            {
                Color = ColorType.Blue,
                X = -20,
                Y = 100,
                Label = "Drugi punkt"
            });
            appModel.Add(new PointModel(appModel)
            {
                Color = ColorType.Red,
                X = 200,
                Y = -50,
                Label = "Trzeci punkt"
            });
            appModel.Add(new PointModel(appModel)
            {
                Color = ColorType.Green,
                X = -500,
                Y = -30,
                Label = "Ostatni punkt"
            });
            return appModel;
        }
    }
}