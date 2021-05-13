using System;
using System.Collections.Generic;

namespace ragther.entity.ViewModels
{
    public class VMTodoUpdatePost
    {
        public int TodoId { get; set; }
        public string imageUrl { get; set; }
        public string Description { get; set; }
        public DateTime UntilWhen { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public List<string> tags { get; set; }
        public List<string> workWiths { get; set; }

        // {
        //     "TodoId" : 37,
        //     "imageUrl": "deneme.jpg",
        //     "Description": "Update Atıyorum worker ekledim",
        //     "UntilWhen": "2022-04-25T15:16:47.191883"
        //     "LocationLatitude": "12.324232"
        //     "LocationLongitude": "12.324232"
        //     "tags": [
        //         "deneme",
        //         "ödev",
        //         "test"
        //     ],
        //     "workWiths": [
        //         "ayse",
        //         "admin",
        //         "ali"
        //     ]
        // }
    }
}