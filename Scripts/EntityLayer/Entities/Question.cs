﻿using System;
namespace EntityLayer.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public string CorrectAnswer { get; set; }
    }
}

