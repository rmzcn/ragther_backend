using System;
using System.Collections.Generic;
using ragther.entity.Abstract;

namespace ragther.entity.ViewModels
{
    public class VMNewTodoPost:IVModel
    {
        public int CreatorUserId { get; set; }
        public string imageUrl { get; set; }
        public string Description { get; set; }
        public DateTime UntilWhen { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string Address { get; set; }
        public List<string> tags { get; set; }

        // {
        //     "CreatorUserId": 5,
        //     "imageUrl": "deneme.jpg",
        //     "Description": "Update Atıyorum worker ekledim",
        //     "UntilWhen": "2021-04-21 08:16:20.658865"
        //     "LocationLatitude": "12.324232"
        //     "LocationLongitude": "12.324232"
        //     "tags": [
        //         "deneme",
        //         "ödev",
        //         "test"
        //     ]
        // }
    }
}