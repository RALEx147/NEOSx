using AppKit;
using System;
using Neo.Core;
using Neo.Implementations.Blockchains.LevelDB;
using Neo.Network;
using Properties;
using Neo.Wallets;
using System.IO;
using Neo;
using System.Threading.Tasks;
using Neo.Cryptography;
using Neo.IO;
using Neo.VM;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using Properties;
namespace NEOSxGUI
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            MainClass mc = new MainClass();
            mc.startBlockChain(args);

        }









        public static LocalNode LocalNode;
        public static Wallet CurrentWallet;
        public void startBlockChain(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //XDocument xdoc = null;
            //try
            //{
            //    xdoc = XDocument.Load("https://neo.org/client/update.xml");
            //}
            //catch { }
            //if (xdoc != null)
            //{
            //    Version version = Assembly.GetExecutingAssembly().GetName().Version;
            //    Version minimum = Version.Parse(xdoc.Element("update").Attribute("minimum").Value);
            //    if (version < minimum)

            //    {
            //        Console.WriteLine("version behind minuim");
            //            using (UpdateDialog dialog = new UpdateDialog(xdoc))
            //            {
            //                dialog.ShowDialog();
            //            }
            //            return;
            //    }

            //}

            //if (!InstallCertificate()) return;

            const string PeerStatePath = "peers.dat";
            if (File.Exists(PeerStatePath))
                using (FileStream fs = new FileStream(PeerStatePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    LocalNode.LoadState(fs);
                }
            
            var chainPath = Properties.Settings.Default.Paths.Chain;
            using (Blockchain.RegisterBlockchain(new LevelDBBlockchain(chainPath)))

            using (LocalNode = new LocalNode())
            {
                LocalNode.UpnpEnabled = true;
                //Application.Run(MainForm = new MainForm(xdoc)); HOW THE FUCK DO I DO THIS IN MAC

                MainClass mc = new MainClass();
                mc.runBlockChain();
                NSApplication.Init();
                NSApplication.Main(args);



            }

        }





        private void runBlockChain()
        {

            Task.Run(() =>
            {
                const string path_acc = "chain.acc";
                if (File.Exists(path_acc))
                {
                    using (FileStream fs = new FileStream(path_acc, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        ImportBlocks(fs);
                    }
                }

                var paths = Directory.EnumerateFiles(".", "chain.*.acc", SearchOption.TopDirectoryOnly).Concat(Directory.EnumerateFiles(".", "chain.*.acc.zip", SearchOption.TopDirectoryOnly)).Select(p => new
                {
                    FileName = Path.GetFileName(p),
                    Start = uint.Parse(Regex.Match(p, @"\d+").Value),
                    IsCompressed = p.EndsWith(".zip")
                }).OrderBy(p => p.Start);

                foreach (var path in paths)
                {
                    if (path.Start > Blockchain.Default.Height + 1) break;
                    if (path.IsCompressed)
                    {
                        using (FileStream fs = new FileStream(path.FileName, FileMode.Open, FileAccess.Read, FileShare.None))
                        using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Read))
                        using (Stream zs = zip.GetEntry(Path.GetFileNameWithoutExtension(path.FileName)).Open())
                        {
                            ImportBlocks(zs, true);
                        }
                    }
                    else
                    {
                        using (FileStream fs = new FileStream(path.FileName, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            ImportBlocks(fs, true);
                        }
                    }
                }
                Blockchain.PersistCompleted += Blockchain_PersistCompleted;
                LocalNode.Start(Properties.Settings.Default.P2P.Port, Properties.Settings.Default.P2P.WsPort);

            });

        }


        public void Blockchain_PersistCompleted(object sender, Block block)
        {
            //if (IsDisposed) return;
            persistence_time = DateTime.UtcNow;
            if (CurrentWallet != null)
            {
                check_nep5_balance = true;
                if (CurrentWallet.GetCoins().Any(p => !p.State.HasFlag(CoinState.Spent) && p.Output.AssetId.Equals(Blockchain.GoverningToken.Hash)) == true)
                    balance_changed = true;
            }

            //this asyncly does action of refreshing the nums at the bottom.
            //BeginInvoke(new Action(RefreshConfirmations));
        }


        private static readonly UInt160 RecycleScriptHash = new[] { (byte)OpCode.PUSHT }.ToScriptHash();
        private bool balance_changed = false;
        private bool check_nep5_balance = false;
        private DateTime persistence_time = DateTime.MinValue;

        public void ChangeWallet(Wallet wallet)
        {
            if (CurrentWallet != null)
            {
                CurrentWallet.BalanceChanged -= CurrentWallet_BalanceChanged;
                if (CurrentWallet is IDisposable disposable)
                    disposable.Dispose();
            }
            CurrentWallet = wallet;
            //listView3.Items.Clear();
            if (CurrentWallet != null)
            {
                foreach (var i in CurrentWallet.GetTransactions().Select(p => new
                {
                    Transaction = Blockchain.Default.GetTransaction(p, out int height),
                    Height = (uint)height
                }).Where(p => p.Transaction != null).Select(p => new
                {
                    p.Transaction,
                    p.Height,
                    Time = Blockchain.Default.GetHeader(p.Height).Timestamp
                }).OrderBy(p => p.Time))
                {
                    //AddTransaction(i.Transaction, i.Height, i.Time);
                    //display in the list all the transaction and the ids

                }
                CurrentWallet.BalanceChanged += CurrentWallet_BalanceChanged;
            }

            if (CurrentWallet != null)
            {
                foreach (WalletAccount account in CurrentWallet.GetAccounts().ToArray())
                {
                    //AddAccount(account);
                    //display in the list all the addresses with the gas/neo
                }
            }
            balance_changed = true;
            check_nep5_balance = true;
        }

        protected void CurrentWallet_BalanceChanged(object sender, BalanceEventArgs e)
        {
            balance_changed = true;
            //this asyncly does action of refreshing the nums at the bottom.
            //BeginInvoke(new Action<Transaction, uint?, uint>(AddTransaction), e.Transaction, e.Height, e.Time);
        }

        private void RefreshConfirmations()
        {
            //foreach (ListViewItem item in listView3.Items)
            //{
            //    uint? height = item.Tag as uint?;
            //    int? confirmations = (int)Blockchain.Default.Height - (int?)height + 1;
            //    if (confirmations <= 0) confirmations = null;
            //    item.SubItems["confirmations"].Text = confirmations?.ToString() ?? Strings.Unconfirmed;
            //}
        }

        private void AddTransaction(Transaction tx, uint? height, uint time)
        {
            int? confirmations = (int)Blockchain.Default.Height - (int?)height + 1;
            if (confirmations <= 0) confirmations = null;
            string confirmations_str = confirmations?.ToString() ?? Strings.Unconfirmed;
            string txid = tx.Hash.ToString();
            /*if (listView3.Items.ContainsKey(txid))
            {
                listView3.Items[txid].Tag = height;
                listView3.Items[txid].SubItems["confirmations"].Text = confirmations_str;
            }
            else
            {
                listView3.Items.Insert(0, new ListViewItem(new[]
                {
                            new ListViewItem.ListViewSubItem
                            {
                                Name = "time",
                                Text = time.ToDateTime().ToString()
                            },
                            new ListViewItem.ListViewSubItem
                            {
                                Name = "hash",
                                Text = txid
                            },
                            new ListViewItem.ListViewSubItem
                            {
                                Name = "confirmations",
                                Text = confirmations_str
                            },
                            //add transaction type to list by phinx
                            new ListViewItem.ListViewSubItem
                            {
                                Name = "txtype",
                                Text = tx.Type.ToString()
                            }
                            //end

                        }, -1)
                {
                    Name = txid,
                    Tag = height
                });
            }
            */
        }







        private void ImportBlocks(Stream stream, bool read_start = false)
        {
            LevelDBBlockchain blockchain = (LevelDBBlockchain)Blockchain.Default;
            using (BinaryReader r = new BinaryReader(stream))
            {
                uint start = read_start ? r.ReadUInt32() : 0;
                uint count = r.ReadUInt32();
                uint end = start + count - 1;
                if (end <= blockchain.Height) return;
                for (uint height = start; height <= end; height++)
                {
                    byte[] array = r.ReadBytes(r.ReadInt32());
                    if (height > blockchain.Height)
                    {

                        Block block = array.AsSerializable<Block>();
                        blockchain.AddBlockDirectly(block);
                    }
                }
            }
        }


        private static void PrintErrorLogs(StreamWriter writer, Exception ex)
        {
            writer.WriteLine(ex.GetType());
            writer.WriteLine(ex.Message);
            writer.WriteLine(ex.StackTrace);
            if (ex is AggregateException ex2)
            {
                foreach (Exception inner in ex2.InnerExceptions)
                {
                    writer.WriteLine();
                    PrintErrorLogs(writer, inner);
                }
            }
            else if (ex.InnerException != null)
            {
                writer.WriteLine();
                PrintErrorLogs(writer, ex.InnerException);
            }
        }
        protected static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            using (FileStream fs = new FileStream("error.log", FileMode.Create, FileAccess.Write, FileShare.None))
            using (StreamWriter w = new StreamWriter(fs))
            {
                PrintErrorLogs(w, (Exception)e.ExceptionObject);
            }
        }






    }
}
