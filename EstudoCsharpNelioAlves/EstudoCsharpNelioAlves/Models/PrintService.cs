﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoCsharpNelioAlves.Models
{
    class PrintService<T>
    {
        private T[] _values = new T[10];
        private int _count = 0;

        public void AddValue(T value)
        {
            if(_count == 10)
            {
                throw new InvalidOperationException("Print Service is full");
            }
            _values[_count] = value;
            _count++;
        }
        public T FirtValue()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Print Service is wmpty");
            }
            return _values[0];
        }
        public void Print()
        {
            Console.Write("[");
            for(int i = 0; i < _count - 1;i++) 
            {
                if (_count == 10)
                {
                    throw new InvalidOperationException("Lotou");
                }
                Console.Write(_values[i]);
                Console.Write(",");
            }
            if(_count > 0)
            {
                Console.Write(_values[_count - 1]);
            }
            Console.WriteLine("]");
        }
    
    
    }

}
