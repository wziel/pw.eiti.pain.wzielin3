using System;
using System.Collections.Generic;

namespace Pw.Eiti.Pain.Wzielin3.Lab2
{
    public class ApplicationModel
    {
        public event EventHandler PointAdded;
        public event EventHandler PointRemoved;

        private List<PointModel> points = new List<PointModel>();

        public IReadOnlyCollection<PointModel> Points { get { return points; } }

        public void AddPoint(PointModel model)
        {
            points.Add(model);
            if(PointAdded != null)
            {
                PointAdded.Invoke(model, null);
            }
        }

        public void RemovePoint(PointModel model)
        {
            if(points.Remove(model) && PointRemoved != null)
            {
                PointRemoved.Invoke(model, null);
            }
        }
    }
}