namespace 号码预测器
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_DrawPrize
    {
        [Key]
        [StringLength(10)]
        public string Lssue { get; set; }

        public int number1 { get; set; }

        public int number2 { get; set; }

        public int number3 { get; set; }

        public int number4 { get; set; }

        public int number5 { get; set; }

        public int number6 { get; set; }

        public int number7 { get; set; }
    }
}
