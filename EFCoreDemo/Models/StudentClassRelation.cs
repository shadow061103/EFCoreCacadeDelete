﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace EFCoreDemo
{
    public partial class StudentClassRelation
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Classes Classes { get; set; }
        public virtual Students Students { get; set; }
    }
}