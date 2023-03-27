namespace SimpleLangInterpreter;

public enum TokenType
{
   EndOfFile,
   Undefined,
   NumberToken,
   BinaryToken,
   LitteralToken,
   StringToken,
   Comment, 
   TypeToken,
   OperationToken,
   KeywordToken,
   WhiteSpace
}