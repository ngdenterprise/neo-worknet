using System.ComponentModel.DataAnnotations;
using System.IO.Abstractions;
using McMaster.Extensions.CommandLineUtils;
using Neo.BlockchainToolkit.Models;
using Neo.Wallets;
using NeoWorkNet.Node;

namespace NeoWorkNet.Commands
{
    partial class StorageCommand
    {
        [Command("update", Description = "Update a value in neo-worknet")]
        internal class Update
        {
            readonly IFileSystem fs;

            public Update(IFileSystem fileSystem)
            {
                this.fs = fileSystem;
            }

            [Argument(1, Description = "Contract name or hash")]
            [Required]
            internal string Contract { get; init; } = string.Empty;

            [Argument(2, Description = "Key")]
            [Required]
            internal string Key { get; init; } = string.Empty;

            [Argument(3, Description = "New value in Hex")]
            [Required]
            internal string Value { get; init; } = string.Empty;

            internal async Task<int> OnExecuteAsync(CommandLineApplication app, IConsole console)
            {
                try
                {
                    var (chain, filename) = await fs.LoadWorknetAsync(app).ConfigureAwait(false);
                    byte[] keyBytes = StorageCommand.GetKeyInBytes(Key);
                    byte[] valueBytes = GetValueInBytes(chain);
                    var node = new WorkNetNode(chain, filename);
                    ContractInfo? contractInfo = StorageCommand.FindContractInfo(chain, Contract);
                    if (contractInfo.Id < 0)
                    {
                        throw new ArgumentException("Updating storage value for native Contracts are not allowed.");
                    }
                    node.UpdateValue(contractInfo, keyBytes, valueBytes);
                    await console.Out.WriteLineAsync($"Storage value updated for key: {Key}");
                    return 0;
                }
                catch (Exception ex)
                {
                    app.WriteException(ex);
                    return 1;
                }
            }

            private byte[] GetValueInBytes(WorknetChain chain)
            {
                byte[] valueBytes;
                if (Value.StartsWith("N"))
                {
                    try
                    {
                        var valueScriptHash = Value.ToScriptHash(chain.BranchInfo.AddressVersion);
                        valueBytes = Neo.IO.Helper.ToArray(valueScriptHash);
                    }
                    catch (System.FormatException)
                    {
                        throw new ArgumentException("Value format is invalid"); ;
                    }

                }
                else if (Value.StartsWith("0x"))
                {
                    valueBytes = Convert.FromHexString(Value.Substring(2));
                }
                else
                {
                    valueBytes = Convert.FromHexString(Value);
                }

                return valueBytes;
            }
        }
    }
}