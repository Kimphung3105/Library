Get-Content .env | ForEach-Object {
    if ($_ -match '^([^#][^=]+)=(.*)$') {
        $name = $matches[1].Trim()
        $value = $matches[2].Trim()
        [Environment]::SetEnvironmentVariable($name, $value, "Process")
    }
}

# Install EF tool
dotnet tool install -g dotnet-ef

# Run scaffolding
dotnet ef dbcontext scaffold $env:Host=ep-icy-hall-agu89sfa-pooler.c-2.eu-central-1.aws.neon.tech; Database=neondb; Username=neondb_owner; Password=npg_GmxOz4L2soVY; SSL Mode=VerifyFull; Channel Binding=Require; Npgsql.EntityFrameworkCore.PostgreSQL --context MyDbContext --no-onconfiguring --schema library --force