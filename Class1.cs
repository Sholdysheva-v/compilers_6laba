//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Лабораторная_1_компиляторы
//{
//    public class LexicalAnalyzer
//    {
//        public enum TokenType
//        {
//            UnsignedInt = 1,         // Целое без знака
//            SignedInt = 3,            // Целое со знаком
//            Identifier = 2,          // Идентификатор
//            Keyword = 14,            // Ключевое слово
//            Separator = 11,          // Разделитель (пробел)
//            AssignmentOperator = 10, // Оператор присваивания
//            EndOfStatement = 16,     // Конец оператора
//            TypeKeyword = 17,        // Ключевое слово типа
//            StructKeyword = 18,      // Ключевое слово struct
//            OpenBrace = 19,          // Открывающая фигурная скобка
//            CloseBrace = 20,         // Закрывающая фигурная скобка
//            DollarSign = 21,         // Знак доллара
//            StringLiteral = 22,      // Строковый литерал
//            PlusOperator = 23,       // Оператор +
//            MinusOperator = 24,      // Оператор -
//            InvalidChar = 99         // Недопустимый символ
//        }

//        public class Token
//        {
//            public int Code { get; set; }
//            public string Type { get; set; }
//            public string Value { get; set; }
//            public int StartPos { get; set; }
//            public int EndPos { get; set; }

//            public Token(int code, string type, string value, int startPos, int endPos)
//            {
//                Code = code;
//                Type = type;
//                Value = value;
//                StartPos = startPos;
//                EndPos = endPos;
//            }

//            public override string ToString()
//            {
//                return $"{Code} - {Type} - {Value} - с {StartPos} по {EndPos} символ";
//            }
//        }

//        private static readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
//        {
//            {"struct", TokenType.StructKeyword},
//            {"string", TokenType.TypeKeyword},
//            {"int", TokenType.TypeKeyword},
//            {"bool", TokenType.TypeKeyword},
//            {"float", TokenType.TypeKeyword}
//        };

//        public List<Token> Analyze(string input)
//        {
//            List<Token> tokens = new List<Token>();
//            int currentPos = 0;
//            int lineNumber = 1;

//            while (currentPos < input.Length)
//            {
//                char currentChar = input[currentPos];

//                // Пропускаем пробелы и переносы строк
//                if (char.IsWhiteSpace(currentChar))
//                {
//                    if (currentChar == '\n')
//                    {
//                        lineNumber++;
//                    }
//                    tokens.Add(new Token(
//                        (int)TokenType.Separator,
//                        "разделитель",
//                        currentChar.ToString(),
//                        currentPos + 1,
//                        currentPos + 1
//                    ));
//                    currentPos++;
//                    continue;
//                }

//                // Обработка строковых литералов
//                if (currentChar == '"')
//                {
//                    StringBuilder sb = new StringBuilder();
//                    int startPos = currentPos + 1;
//                    sb.Append(currentChar);
//                    currentPos++;

//                    bool escape = false;
//                    bool closed = false;

//                    while (currentPos < input.Length)
//                    {
//                        currentChar = input[currentPos];
//                        sb.Append(currentChar);

//                        if (currentChar == '\\' && !escape)
//                        {
//                            escape = true;
//                        }
//                        else if (currentChar == '"' && !escape)
//                        {
//                            closed = true;
//                            currentPos++;
//                            break;
//                        }
//                        else
//                        {
//                            escape = false;
//                        }

//                        currentPos++;
//                    }

//                    if (!closed)
//                    {
//                        tokens.Add(new Token(
//                            (int)TokenType.InvalidChar,
//                            "незакрытый строковый литерал",
//                            sb.ToString(),
//                            startPos,
//                            currentPos
//                        ));
//                    }
//                    else
//                    {
//                        tokens.Add(new Token(
//                            (int)TokenType.StringLiteral,
//                            "строковый литерал",
//                            sb.ToString(),
//                            startPos,
//                            currentPos
//                        ));
//                    }
//                    continue;
//                }

//                // Обработка операторов + и -
//                if (currentChar == '+' || currentChar == '-')
//                {
//                    // Проверяем, является ли минус частью числа
//                    if (currentChar == '-' && currentPos + 1 < input.Length && char.IsDigit(input[currentPos + 1]))
//                    {
//                        // Обрабатываем как число со знаком
//                        StringBuilder sb = new StringBuilder();
//                        int startPos = currentPos + 1;
//                        sb.Append(currentChar);
//                        currentPos++;

//                        while (currentPos < input.Length && char.IsDigit(input[currentPos]))
//                        {
//                            sb.Append(input[currentPos]);
//                            currentPos++;
//                        }

//                        tokens.Add(new Token(
//                            (int)TokenType.SignedInt,
//                            "целое со знаком",
//                            sb.ToString(),
//                            startPos,
//                            currentPos
//                        ));
//                    }
//                    else
//                    {
//                        // Обрабатываем как отдельный оператор
//                        tokens.Add(new Token(
//                            currentChar == '+' ? (int)TokenType.PlusOperator : (int)TokenType.MinusOperator,
//                            currentChar == '+' ? "оператор +" : "оператор -",
//                            currentChar.ToString(),
//                            currentPos + 1,
//                            currentPos + 1
//                        ));
//                        currentPos++;
//                    }
//                    continue;
//                }

//                // Обработка знака доллара
//                if (currentChar == '$')
//                {
//                    tokens.Add(new Token(
//                        (int)TokenType.DollarSign,
//                        "знак доллара",
//                        "$",
//                        currentPos + 1,
//                        currentPos + 1
//                    ));
//                    currentPos++;
//                    continue;
//                }

//                // Проверяем ключевые слова и идентификаторы
//                if (char.IsLetter(currentChar) && IsLatinLetter(currentChar))
//                {
//                    StringBuilder sb = new StringBuilder();
//                    int startPos = currentPos + 1;

//                    while (currentPos < input.Length && (IsLatinLetterOrDigit(input[currentPos]) || input[currentPos] == '_'))
//                    {
//                        sb.Append(input[currentPos]);
//                        currentPos++;
//                    }

//                    string word = sb.ToString();
//                    int endPos = currentPos;

//                    if (Keywords.TryGetValue(word, out TokenType keywordType))
//                    {
//                        tokens.Add(new Token(
//                            (int)keywordType,
//                            keywordType == TokenType.StructKeyword ? "ключевое слово (struct)" : "ключевое слово типа",
//                            word,
//                            startPos,
//                            endPos
//                        ));
//                    }
//                    else
//                    {
//                        tokens.Add(new Token(
//                            (int)TokenType.Identifier,
//                            "идентификатор",
//                            word,
//                            startPos,
//                            endPos
//                        ));
//                    }
//                    continue;
//                }

//                // Проверяем числа без знака
//                if (char.IsDigit(currentChar))
//                {
//                    StringBuilder sb = new StringBuilder();
//                    int startPos = currentPos + 1;

//                    while (currentPos < input.Length && char.IsDigit(input[currentPos]))
//                    {
//                        sb.Append(input[currentPos]);
//                        currentPos++;
//                    }

//                    tokens.Add(new Token(
//                        (int)TokenType.UnsignedInt,
//                        "целое без знака",
//                        sb.ToString(),
//                        startPos,
//                        currentPos
//                    ));
//                    continue;
//                }

//                // Проверяем специальные символы
//                switch (currentChar)
//                {
//                    case '{':
//                        tokens.Add(new Token(
//                            (int)TokenType.OpenBrace,
//                            "открывающая фигурная скобка",
//                            "{",
//                            currentPos + 1,
//                            currentPos + 1
//                        ));
//                        currentPos++;
//                        continue;
//                    case '}':
//                        tokens.Add(new Token(
//                            (int)TokenType.CloseBrace,
//                            "закрывающая фигурная скобка",
//                            "}",
//                            currentPos + 1,
//                            currentPos + 1
//                        ));
//                        currentPos++;
//                        continue;
//                    case ';':
//                        tokens.Add(new Token(
//                            (int)TokenType.EndOfStatement,
//                            "конец оператора",
//                            ";",
//                            currentPos + 1,
//                            currentPos + 1
//                        ));
//                        currentPos++;
//                        continue;
//                    default:
//                        if (IsCyrillic(currentChar))
//                        {
//                            tokens.Add(new Token(
//                                (int)TokenType.InvalidChar,
//                                "недопустимый символ (русская буква)",
//                                currentChar.ToString(),
//                                currentPos + 1,
//                                currentPos + 1
//                            ));
//                        }
//                        else
//                        {
//                            tokens.Add(new Token(
//                                (int)TokenType.InvalidChar,
//                                "недопустимый символ",
//                                currentChar.ToString(),
//                                currentPos + 1,
//                                currentPos + 1
//                            ));
//                        }
//                        currentPos++;
//                        continue;
//                }
//            }

//            return tokens;
//        }

//        private bool IsLatinLetter(char c)
//        {
//            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
//        }

//        private bool IsLatinLetterOrDigit(char c)
//        {
//            return IsLatinLetter(c) || char.IsDigit(c);
//        }

//        private bool IsCyrillic(char c)
//        {
//            return (c >= 'А' && c <= 'я') || c == 'ё' || c == 'Ё';
//        }
//    }
//}