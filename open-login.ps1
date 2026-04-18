# Open the login.html file in the default browser from the repository root
# Usage: In PowerShell run: .\open-login.ps1
$path = Join-Path -Path (Get-Location) -ChildPath 'login.html'
if (Test-Path $path) {
    Start-Process -FilePath $path
} else {
    Write-Error "login.html not found in current folder: $path"
}
