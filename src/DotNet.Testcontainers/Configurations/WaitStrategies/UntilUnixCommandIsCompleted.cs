namespace DotNet.Testcontainers.Configurations
{
  using System.Globalization;
  using System.Text;
  using System.Threading.Tasks;
  using DotNet.Testcontainers.Containers;
  using Microsoft.Extensions.Logging;

  internal class UntilUnixCommandIsCompleted : IWaitUntil
  {
    private readonly string[] command;

    public UntilUnixCommandIsCompleted(string command)
      : this("/bin/sh", "-c", command)
    {
    }

    public UntilUnixCommandIsCompleted(params string[] command)
    {
      this.command = command;
    }

    public virtual async Task<bool> Until(ITestcontainersContainer container, ILogger logger)
    {
      var execResult = await container.ExecAsync(this.command)
        .ConfigureAwait(false);

      return 0L.Equals(execResult.ExitCode);
    }
  }
}
