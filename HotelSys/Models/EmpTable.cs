using System;
using System.Collections.Generic;

#nullable disable

namespace HotelSys.Models
{
    public partial class EmpTable
    {
        public EmpTable()
        {
            JopEmpTableIdEmpNavigations = new HashSet<JopEmpTable>();
            JopEmpTableIdJobNameNavigations = new HashSet<JopEmpTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string NumIdentity { get; set; }
        public int? IdSub { get; set; }

        public virtual ICollection<JopEmpTable> JopEmpTableIdEmpNavigations { get; set; }
        public virtual ICollection<JopEmpTable> JopEmpTableIdJobNameNavigations { get; set; }
    }
}
