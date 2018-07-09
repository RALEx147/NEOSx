using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Neo.IO.Data.LevelDB
{
    //internal enum CompressionType : byte
    //{
    //    kNoCompression = 0x0,
    //    kSnappyCompression = 0x1
    //}

    public static class NativeRocksDB
    {

        #region Logger
        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_logger_create(IntPtr /* Action<string> */ logger);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_logger_destroy(IntPtr /* logger*/ option);
        #endregion

        #region DB
        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_open(IntPtr /* Options*/ options, string name, out IntPtr error);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_close(IntPtr /*DB */ db);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_put(IntPtr /* DB */ db, IntPtr /* WriteOptions*/ options, byte[] key, UIntPtr keylen, byte[] val, UIntPtr vallen, out IntPtr errptr);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_delete(IntPtr /* DB */ db, IntPtr /* WriteOptions*/ options, byte[] key, UIntPtr keylen, out IntPtr errptr);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_write(IntPtr /* DB */ db, IntPtr /* WriteOptions*/ options, IntPtr /* WriteBatch */ batch, out IntPtr errptr);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_get(IntPtr /* DB */ db, IntPtr /* ReadOptions*/ options, byte[] key, UIntPtr keylen, out UIntPtr vallen, out IntPtr errptr);

        //[DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //static extern void rocksdb_approximate_sizes(IntPtr /* DB */ db, int num_ranges, byte[] range_start_key, long range_start_key_len, byte[] range_limit_key, long range_limit_key_len, out long sizes);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_create_iterator(IntPtr /* DB */ db, IntPtr /* ReadOption */ options);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_create_snapshot(IntPtr /* DB */ db);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_release_snapshot(IntPtr /* DB */ db, IntPtr /* SnapShot*/ snapshot);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_property_value(IntPtr /* DB */ db, string propname);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_repair_db(IntPtr /* Options*/ options, string name, out IntPtr error);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_destroy_db(IntPtr /* Options*/ options, string name, out IntPtr error);

        #region extensions 

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_free(IntPtr /* void */ ptr);

        #endregion


        #endregion

        #region Env
        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_create_default_env();

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_env_destroy(IntPtr /*Env*/ cache);
        #endregion

        #region Iterator
        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_iter_destroy(IntPtr /*Iterator*/ iterator);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool rocksdb_iter_valid(IntPtr /*Iterator*/ iterator);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_iter_seek_to_first(IntPtr /*Iterator*/ iterator);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_iter_seek_to_last(IntPtr /*Iterator*/ iterator);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_iter_seek(IntPtr /*Iterator*/ iterator, byte[] key, UIntPtr length);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_iter_next(IntPtr /*Iterator*/ iterator);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_iter_prev(IntPtr /*Iterator*/ iterator);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_iter_key(IntPtr /*Iterator*/ iterator, out UIntPtr length);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_iter_value(IntPtr /*Iterator*/ iterator, out UIntPtr length);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_iter_get_error(IntPtr /*Iterator*/ iterator, out IntPtr error);
        #endregion

        #region Options
        //[DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [DllImport("librocksdb", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_options_create();

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_destroy(IntPtr /*Options*/ options);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_create_if_missing(IntPtr /*Options*/ options, [MarshalAs(UnmanagedType.U1)] bool o);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_error_if_exists(IntPtr /*Options*/ options, [MarshalAs(UnmanagedType.U1)] bool o);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_info_log(IntPtr /*Options*/ options, IntPtr /* Logger */ logger);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_paranoid_checks(IntPtr /*Options*/ options, [MarshalAs(UnmanagedType.U1)] bool o);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_env(IntPtr /*Options*/ options, IntPtr /*Env*/ env);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_write_buffer_size(IntPtr /*Options*/ options, UIntPtr size);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_max_open_files(IntPtr /*Options*/ options, int max);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_cache(IntPtr /*Options*/ options, IntPtr /*Cache*/ cache);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_block_size(IntPtr /*Options*/ options, UIntPtr size);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_block_restart_interval(IntPtr /*Options*/ options, int interval);

        //[DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void rocksdb_options_set_compression(IntPtr /*Options*/ options, CompressionType level);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_comparator(IntPtr /*Options*/ options, IntPtr /*Comparator*/ comparer);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_options_set_filter_policy(IntPtr /*Options*/ options, IntPtr /*FilterPolicy*/ policy);
        #endregion

        #region ReadOptions
        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_readoptions_create();

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_readoptions_destroy(IntPtr /*ReadOptions*/ options);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_readoptions_set_verify_checksums(IntPtr /*ReadOptions*/ options, [MarshalAs(UnmanagedType.U1)] bool o);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_readoptions_set_fill_cache(IntPtr /*ReadOptions*/ options, [MarshalAs(UnmanagedType.U1)] bool o);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_readoptions_set_snapshot(IntPtr /*ReadOptions*/ options, IntPtr /*SnapShot*/ snapshot);
        #endregion

        #region WriteBatch
        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_writebatch_create();

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_writebatch_destroy(IntPtr /* WriteBatch */ batch);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_writebatch_clear(IntPtr /* WriteBatch */ batch);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_writebatch_put(IntPtr /* WriteBatch */ batch, byte[] key, UIntPtr keylen, byte[] val, UIntPtr vallen);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_writebatch_delete(IntPtr /* WriteBatch */ batch, byte[] key, UIntPtr keylen);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_writebatch_iterate(IntPtr /* WriteBatch */ batch, object state, Action<object, byte[], int, byte[], int> put, Action<object, byte[], int> deleted);
        #endregion

        #region WriteOptions
        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_writeoptions_create();

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_writeoptions_destroy(IntPtr /*WriteOptions*/ options);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_writeoptions_set_sync(IntPtr /*WriteOptions*/ options, [MarshalAs(UnmanagedType.U1)] bool o);
        #endregion

        #region Cache 
        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rocksdb_cache_create_lru(int capacity);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_cache_destroy(IntPtr /*Cache*/ cache);
        #endregion

        #region Comparator

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr /* rocksdb_comparator_t* */
            rocksdb_comparator_create(
            IntPtr /* void* */ state,
            IntPtr /* void (*)(void*) */ destructor,
            IntPtr
                /* int (*compare)(void*,
                                  const char* a, size_t alen,
                                  const char* b, size_t blen) */
                compare,
            IntPtr /* const char* (*)(void*) */ name);

        [DllImport("librocksdb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rocksdb_comparator_destroy(IntPtr /* rocksdb_comparator_t* */ cmp);

        #endregion
    }

    //internal static class NativeHelper
    //{
    //    public static void CheckError(IntPtr error)
    //    {
    //        if (error != IntPtr.Zero)
    //        {
    //            string message = Marshal.PtrToStringAnsi(error);
    //            Native.rocksdb_free(error);
    //            throw new LevelDBException(message);
    //        }
    //    }
    //}
}
