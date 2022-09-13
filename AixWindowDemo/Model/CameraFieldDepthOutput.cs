namespace AixWindowDemo.Model
{
    /* 计算景深输出 */
    public  class CameraFieldDepthOutput
    {
        public double frontFieldDepth { set; get; }   //前景深
        public double behindFieldDepth { set; get; }  //后景深
        public double fieldDepth { set; get; }  //景深
    }
}
