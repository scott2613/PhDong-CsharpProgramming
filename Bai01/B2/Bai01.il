// Mã MSIL tương đương với chương trình B1.
// Có thể mở file này bằng ildasm.exe hoặc lắp ráp lại bằng ilasm.exe.
.assembly extern mscorlib
{
  .ver 4:0:0:0
}
.assembly Bai01
{
  .ver 1:0:0:0
}
.module Bai01.exe

.class private auto ansi beforefieldinit Program
       extends [mscorlib]System.Object
{
  .method private hidebysig static void Main() cil managed
  {
    .entrypoint
    .maxstack 2
    .locals init ([0] string hoTen)

    ldstr "Nhap ho ten cua ban: "
    call void [mscorlib]System.Console::Write(string)
    call string [mscorlib]System.Console::ReadLine()
    stloc.0

    ldstr "Ho ten ban da nhap: {0}"
    ldloc.0
    call void [mscorlib]System.Console::WriteLine(string, object)
    ret
  }
}
