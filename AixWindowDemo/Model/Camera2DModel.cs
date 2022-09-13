namespace AixWindowDemo.Model
{
    /* 相机焦距计算和倍率计算模型 */

    public enum Camera2DFocType
    {
        Focal,
        Scale
    }
    public class Camera2DFocModel
    {
        public Camera2DFocType focType { set; get; }
        public double value { set; get; }
    }

    /* 相机飞拍计算中，速度和曝光时间计算模型 */
    //创建该模型是为了减少耦合
    public enum Camera2DSpeedType
    {
        Speed,
        Exposure
    }
    public class Camera2DSpeedModel
    {
        public double pixelNumber { set; get; }  //容许像素拖影个数
        public Camera2DSpeedType speedType { set; get; }  //计算类型，以速度计算还是曝光时间计算
        public double value { set; get; }  //值
    }

    /* 相机输入参数模型 */
    public class Camera2DModel
    {
        public double width { set; get; }  //相机分辨率宽
        public double height { set; get; }  //相机分辨率长
        public double pixelSize { set; get; }  //像元大小
        public Camera2DFocModel camera2DFocModel { set; get; }  //以倍率计算还是以焦距计算
        public double distance { set; get; }   //工作距离
        public Camera2DSpeedModel camera2DSpeedModel { set; get; }  //飞拍
    }
}
