function Invoke-Command {
  <#
    .DESCRIPTION
    Wrapper function to use when you want to exit on error.

    .PARAMETER ScriptBlock
    Script to execute

    .EXAMPLE
    Invoke-Command {
      <SCRIPT_HERE>
    }
  #>
  param (
    [Parameter(Mandatory = $true)]
    [scriptblock] $ScriptBlock
  )
  & @ScriptBlock

  if ($LASTEXITCODE -ne 0) {
    throw "Exited with exit code $LASTEXITCODE"
  }
}

Export-ModuleMember -Function Invoke-Command
