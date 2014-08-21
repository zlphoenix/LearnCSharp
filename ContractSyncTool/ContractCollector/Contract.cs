namespace Zlphoenix.OfficeTool
{
    /// <summary>
    /// 联系人信息
    /// </summary>
    public class Contract
    {
        /// <summary>
        /// 全公司序号
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        ///工号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 按照当前联系人信息重写ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("序号:{0},工号:{1},姓名:{2},职务:{3},办公电话:{4},移动电话:{5},电子邮件:{6},部门:{7}",
                Seq, Code, Name, Title, Tel, Mobile, Email, Dept);
        }
    }

}
