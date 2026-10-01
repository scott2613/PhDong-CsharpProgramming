<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

void Main()
{
    int[] daySo = { 50, 42, 16, 3, 9, 8, 12, 7, 24, 0 };

    // Query Syntax: lọc các số đồng thời chia hết cho 4 và 3.
    var chiaHet =
        from so in daySo
        where so % 4 == 0 && so % 3 == 0
        select so;

    // Method Syntax: biến đổi số chẵn bằng cách chia 2, giữ nguyên số lẻ.
    var bienDoi = daySo.Select(so => so % 2 == 0 ? so / 2 : so);

    chiaHet.Dump("Chia hết cho 4 và 3");
    bienDoi.Dump("Chẵn chia 2, lẻ giữ nguyên");
}
