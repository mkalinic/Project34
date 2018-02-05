﻿using IGG.TenderPortal.Common;
using System;

namespace IGG.TenderPortal.DtoModel
{
    public class MilestoneModel
    {
        public int MilestoneId { get; set; }
        public int TenderId { get; set; }
        public MilestoneType MilestoneType { get; set; }
        public DateTime WillBeAt { get; set; }
        public bool IsDone { get; set; }
    }
}