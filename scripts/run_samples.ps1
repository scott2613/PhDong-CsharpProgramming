$ErrorActionPreference = 'Stop'

$workspace = Split-Path -Parent $PSScriptRoot
$outputDirectory = Join-Path $workspace 'artifacts\sample-output'
New-Item -ItemType Directory -Force -Path $outputDirectory | Out-Null

$samples = [ordered]@{
    'Bai01' = "Nguyễn Huỳnh Phương Đông`n"
    'Bai02' = "Nguyễn Huỳnh Phương Đông`n"
    'Bai03' = "7`n3`n"
    'Bai04' = "7`n3`n"
    'Bai05' = "1`n9`n16`n2`n3`n4`n"
    'Bai06' = "5`n9`n2`n"
    'Bai07' = "17`n"
    'Bai08' = "1.5`n-2`n"
    'Bai09' = "4.5`n-3`n4.5`n"
    'Bai10' = "level`n"
    'Bai11' = "Nguyễn Huỳnh Phương Đông`n"
    'Bai12' = "Nguyễn Huỳnh Phương Đông`n"
    'Bai13' = "3124411071`nNguyễn Huỳnh Phương Đông`nTP. Hồ Chí Minh`n2`n"
    'Bai14' = "Nguyễn Văn A`n10000000`n2`n"
    'Bai15' = "5`n4`n2`n11`n-3`n8`n"
    'Bai16' = "3`nTrần Bình`nNguyễn An`nLê Đông`n"
    'Bai17' = "3`n4`n"
}

$successCount = 0
foreach ($exercise in $samples.Keys) {
    $projectPath = Join-Path $workspace "$exercise\$exercise.csproj"
    $startInfo = [System.Diagnostics.ProcessStartInfo]::new()
    $startInfo.FileName = 'dotnet'
    $startInfo.ArgumentList.Add('run')
    $startInfo.ArgumentList.Add('--project')
    $startInfo.ArgumentList.Add($projectPath)
    $startInfo.ArgumentList.Add('--no-build')
    $startInfo.UseShellExecute = $false
    $startInfo.RedirectStandardInput = $true
    $startInfo.RedirectStandardOutput = $true
    $startInfo.RedirectStandardError = $true
    $startInfo.StandardOutputEncoding = [System.Text.Encoding]::UTF8
    $startInfo.StandardErrorEncoding = [System.Text.Encoding]::UTF8

    # Seed cố định giúp kết quả ma trận của Bài 17 lặp lại được trong báo cáo.
    if ($exercise -eq 'Bai17') {
        $startInfo.Environment['LAB01_SEED'] = '2026'
    }

    $process = [System.Diagnostics.Process]::new()
    $process.StartInfo = $startInfo
    [void]$process.Start()
    $process.StandardInput.Write($samples[$exercise])
    $process.StandardInput.Close()
    $standardOutput = $process.StandardOutput.ReadToEnd()
    $standardError = $process.StandardError.ReadToEnd()
    $process.WaitForExit()

    if ($process.ExitCode -ne 0) {
        throw "$exercise chạy lỗi (exit code $($process.ExitCode)):`n$standardError"
    }

    $transcript = "===== $exercise =====`r`n$standardOutput"
    $outputPath = Join-Path $outputDirectory "$exercise.txt"
    [System.IO.File]::WriteAllText($outputPath, $transcript, [System.Text.UTF8Encoding]::new($false))
    $successCount++
    Write-Host "[ĐẠT] $exercise"
}

Write-Host "$successCount/17 bài chạy thành công"

