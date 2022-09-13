namespace AixWindowDemo.Model
{
    public class CameraLineOutput
    {
        public double sensorSizeWidth { set; get; }  //靶面大小宽
        public double sensorSizeHeight { set; get; }  //靶面大小长
        public double fovWidth { set; get; }  //视野大小宽
        public double fovHeight { set; get; }  //视野大小长
        public double pixelAccuracy { set; get; }  //单像素精度
        public double lensResolution { set; get; }  //镜头解析力
        public double magnification { set; get; }  //等效倍率
        public double lineFrequency { set; get; }  //行频
    }
}
