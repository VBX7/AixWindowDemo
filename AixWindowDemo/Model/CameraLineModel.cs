namespace AixWindowDemo.Model
{
    public class CameraLineModel
    {
        public double width { set; get; }  //相机分辨率宽
        public double height { set; get; }  //相机分辨率长
        public double pixelSize { set; get; }  //像元大小
        public Camera2DFocModel camera2DFocModel { set; get; }  //以倍率计算还是以焦距计算
        public double distance { set; get; }   //工作距离
        public double speed { set; get; }  //速度
    }
}
