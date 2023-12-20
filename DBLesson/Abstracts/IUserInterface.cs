﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLesson.Abstracts
{
    public interface IUserInterface
    {
        public void Write(string str);
        public void WriteLine(string str);
        public string ReadLine();
    }
}
