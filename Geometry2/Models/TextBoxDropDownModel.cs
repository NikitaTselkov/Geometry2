﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.Models
{
	public class TextBoxDropDownModel
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public TextBoxDropDownModel() { }

		public override string ToString()
		{
			return Name;
		}
	}
}
