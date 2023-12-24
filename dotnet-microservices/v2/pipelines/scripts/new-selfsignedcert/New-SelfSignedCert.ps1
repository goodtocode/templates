####################################################################################
# To execute
#   1. Run Powershell as ADMINistrator
#   2. In powershell, set security polilcy for this script: 
#      Set-ExecutionPolicy Unrestricted -Scope Process -Force
#   3. Change directory to the script folder:
#      CD C:\Temp (wherever your script is)
#   4. In powershell, run script: 
#      .\New-SelfSignedCert.ps1 -Path Certs\ -File b2c-dev-SAML.pfx -Domain b2c-dev-Saml.MyCologin.onmicrosoft.com
####################################################################################

param (
	[Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
 	[string]$Path = $(throw '-Path is a required parameter.'), #.\Certs
	[Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
 	[string]$File = $(throw '-File is a required parameter.'), #MyCert.pfx
    [Parameter(Mandatory=$true,ValueFromPipelineByPropertyName=$true)]
	[string]$Domain = $(throw '-Domain is a required parameter.'), #dev.mydomain.com, yourappname.yourtenant.onmicrosoft.com
	[string]$CertStoreLocation = 'Cert:\LocalMachine\My'
)

####################################################################################
Set-ExecutionPolicy Unrestricted -Scope Process -Force
$VerbosePreference = 'SilentlyContinue' # 'SilentlyContinue' # 'Continue'
[String]$ThisScript = $MyInvocation.MyCommand.Path
[String]$ThisDir = Split-Path $ThisScript
Set-Location $ThisDir # Ensure our location is correct, so we can use relative paths
Write-Host "*****************************"
Write-Host "*** Starting: $ThisScript On: $(Get-Date)"
Write-Host "*****************************"
####################################################################################
# Imports
Import-Module ".\Powershell-Library\Code.psm1"
Import-Module ".\Powershell-Library\System.psm1"
$crlf = "`r`n"

$Path = Remove-Suffix -String $Path -Remove "\"
$File = Remove-Prefix -String $File -Remove "." 
$File = Remove-Prefix -String $File -Remove "." 
$File = Remove-Prefix -String $File -Remove "\"
[String] $Password = New-Guid
[String] $FilePath = "$Path\$File"

New-Path $Path

# Remove existing cert
$foundCert = Get-ChildItem -Path $CertStoreLocation | Where-Object Subject -eq "CN=$Domain" | Select-Object *
if($foundCert.Thumbprint.Length -gt 0)
{    
    ## Found, remove it now
    $thumbprint=$foundCert.Thumbprint
    Write-Verbose "$crlf[Verbose] Found thumbprint $thumbprint $crlf"
    Get-ChildItem "Cert:\LocalMachine\My\$thumbprint" | Remove-Item
    Write-Verbose "$crlf[Verbose] Removed thumbprint $thumbprint $crlf"
    $thumbprint = ""
}

# Create
$Certificate = New-SelfSignedCertificate `
    -KeyExportPolicy Exportable `
    -Subject "CN=$Domain" `
    -KeyAlgorithm RSA `
    -KeyLength 2048 `
    -KeyUsage DigitalSignature `
    -NotAfter (Get-Date).AddMonths(12) `
    -CertStoreLocation $CertStoreLocation
$thumbprint=$Certificate.Thumbprint
Write-Host "$crlf[Info] Created $CertStoreLocation with thumbprint $thumbprint $crlf"

# Export Pfx
$securePw = ConvertTo-SecureString -String $Password -Force AsPlainText
Export-PfxCertificate -Cert "$CertStoreLocation\$thumbprint" -FilePath $FilePath -Password $securePw
Write-Host "$crlf[Info] Created $FilePath with password $Password $crlf"

# Export Cer
Export-Certificate -Cert "$CertStoreLocation\$thumbprint" -Filepath "$FilePath.cer" #-Type CERT -NoClobber
Write-Host "$crlf[Info] Created $FilePath.cer $crlf"
