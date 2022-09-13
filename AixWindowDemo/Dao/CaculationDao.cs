using AixWindowDemo.Model;

namespace AixWindowDemo.Dao
{
    /* 这个是逻辑层 */
    public class CaculationDao
    {
        //计算靶面大小，传入分辨率高，分辨率宽，像元大小
        public double[] SensorSize(double pixelHeight, double pixelWeight, double pixelSize)
        {
            double[] sensorS = new double[2];  //sensorS[0]为高度，sensor[1]为宽度
            sensorS[0] = pixelHeight * pixelSize / 1000.0;
            sensorS[1] = pixelWeight * pixelSize / 1000.0;
            return sensorS;
        }

        //计算视野，传入焦距，工作距离，靶面尺寸数组（0为高度，1为宽度）
        public double[] Fov(int focal, double WD, double[] sensorSize)
        {
            double[] fov = new double[2];  //fov[0]为高度，fov[1]为宽度
            fov[0] = sensorSize[0] * WD / focal;
            fov[1] = sensorSize[1] * WD / focal;
            return fov;
        }

        //计算视野，传入倍率，工作距离，靶面尺寸数组（0为高度，1为宽度）
        public double[] Fov(double magni, double WD, double[] sensorSize)
        {
            double[] fov = new double[2];  //fov[0]为高度，fov[1]为宽度
            fov[0] = sensorSize[0] / magni;
            fov[1] = sensorSize[1] / magni;
            return fov;
        }

        //计算景深，仅供参考
        public void fieldDepth(CameraFieldDepthInput fieldIndex ,out double frontDepth, out double behindDepth, out double depth)
        {
            double circle = fieldIndex.circleDiameter;  //最小弥散圆
            double distance = fieldIndex.distance;  //工作距离
            double fno = fieldIndex.fno;  //光圈值
            double focal = fieldIndex.focal;  //焦距

            frontDepth = (fno * circle * distance * distance) / (focal * focal + fno * circle * distance);  //前景深
            behindDepth = (fno * circle * distance * distance) / (focal * focal - fno * circle * distance);  //后景深
            depth = frontDepth + behindDepth;  //景深
        }

    }
}
