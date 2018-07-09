using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Neo.IO.Data.LevelDB
{
    public enum CompressionType : byte
    {
        kNoCompression = 0x0,
        kSnappyCompression = 0x1
    }

    public static class Native
    {
        #region DB
        public static IntPtr leveldb_open(IntPtr /* Options*/ options, string name, out IntPtr error) {
            return NativeRocksDB.rocksdb_open(options, name,out error);
        }
        public static void leveldb_close(IntPtr /*DB */ db) {
            NativeRocksDB.rocksdb_close(db);
        }
        public static void leveldb_put(IntPtr /* DB */ db, IntPtr /* WriteOptions*/ options, byte[] key, UIntPtr keylen, byte[] val, UIntPtr vallen, out IntPtr errptr) {
            NativeRocksDB.rocksdb_put(db, options, key, keylen, val, vallen, out errptr);
        }
        public static void leveldb_delete(IntPtr /* DB */ db, IntPtr /* WriteOptions*/ options, byte[] key, UIntPtr keylen, out IntPtr errptr) {
            NativeRocksDB.rocksdb_delete(db, options, key, keylen, out errptr);
        }
        public static void leveldb_write(IntPtr /* DB */ db, IntPtr /* WriteOptions*/ options, IntPtr /* WriteBatch */ batch, out IntPtr errptr) {
            NativeRocksDB.rocksdb_write(db, options, batch, out errptr);
        }
        public static IntPtr leveldb_get(IntPtr /* DB */ db, IntPtr /* ReadOptions*/ options, byte[] key, UIntPtr keylen, out UIntPtr vallen, out IntPtr errptr) {
            return NativeRocksDB.rocksdb_get(db, options, key, keylen, out vallen, out errptr);
        }
        public static IntPtr leveldb_create_iterator(IntPtr /* DB */ db, IntPtr /* ReadOption */ options) {
            return NativeRocksDB.rocksdb_create_iterator(db, options);
        }
        public static IntPtr leveldb_create_snapshot(IntPtr /* DB */ db) {
            return NativeRocksDB.rocksdb_create_snapshot(db);
        }
        public static void leveldb_release_snapshot(IntPtr /* DB */ db, IntPtr /* SnapShot*/ snapshot) {
            NativeRocksDB.rocksdb_release_snapshot(db, snapshot);
        }
        public static IntPtr leveldb_property_value(IntPtr /* DB */ db, string propname) {
            return NativeRocksDB.rocksdb_property_value(db, propname);
        }
        public static void leveldb_repair_db(IntPtr /* Options*/ options, string name, out IntPtr error) {
            NativeRocksDB.rocksdb_repair_db(options, name, out error);
        }
        public static void leveldb_destroy_db(IntPtr /* Options*/ options, string name, out IntPtr error) {
            NativeRocksDB.rocksdb_destroy_db(options, name, out error);
        }

        #region extensions
        public static void leveldb_free(IntPtr /* void */ ptr) {
            NativeRocksDB.rocksdb_free(ptr);
        }
        #endregion
        #endregion

        #region Env
        public static IntPtr leveldb_create_default_env() {
            return NativeRocksDB.rocksdb_create_default_env();
        }
        public static void leveldb_env_destroy(IntPtr /*Env*/ cache) {
            NativeRocksDB.rocksdb_env_destroy(cache);
        }
        #endregion

        #region Iterator
        public static void leveldb_iter_destroy(IntPtr /*Iterator*/ iterator) {
           NativeRocksDB.rocksdb_iter_destroy(iterator);
        }
        public static bool leveldb_iter_valid(IntPtr /*Iterator*/ iterator) {
           return NativeRocksDB.rocksdb_iter_valid(iterator);
        }
        public static void leveldb_iter_seek_to_first(IntPtr /*Iterator*/ iterator) {
           NativeRocksDB.rocksdb_iter_seek_to_first(iterator);
        }
        public static void leveldb_iter_seek_to_last(IntPtr /*Iterator*/ iterator) {
           NativeRocksDB.rocksdb_iter_seek_to_last(iterator);
        }
        public static void leveldb_iter_seek(IntPtr /*Iterator*/ iterator, byte[] key, UIntPtr length) {
           NativeRocksDB.rocksdb_iter_seek(iterator, key, length);
        }
        public static void leveldb_iter_next(IntPtr /*Iterator*/ iterator) {
           NativeRocksDB.rocksdb_iter_next(iterator);
        }
        public static void leveldb_iter_prev(IntPtr /*Iterator*/ iterator) {
           NativeRocksDB.rocksdb_iter_prev(iterator);
        }
        public static IntPtr leveldb_iter_key(IntPtr /*Iterator*/ iterator, out UIntPtr length) {
           return NativeRocksDB.rocksdb_iter_key(iterator, out length);
        }
        public static IntPtr leveldb_iter_value(IntPtr /*Iterator*/ iterator, out UIntPtr length) {
           return NativeRocksDB.rocksdb_iter_value(iterator, out length);
        }
        public static void leveldb_iter_get_error(IntPtr /*Iterator*/ iterator, out IntPtr error) {
           NativeRocksDB.rocksdb_iter_get_error(iterator, out error);
        }
        #endregion

        #region Options
        public static IntPtr leveldb_options_create() {
           return NativeRocksDB.rocksdb_options_create();
        }
        public static void leveldb_options_destroy(IntPtr /*Options*/ options) {
           NativeRocksDB.rocksdb_options_destroy(options);
        }

        public static void leveldb_options_set_create_if_missing(IntPtr /*Options*/ options, [MarshalAs(UnmanagedType.U1)] bool o) {
           NativeRocksDB.rocksdb_options_set_create_if_missing(options, o);
        }
        public static void leveldb_options_set_error_if_exists(IntPtr /*Options*/ options, [MarshalAs(UnmanagedType.U1)] bool o) {
           NativeRocksDB.rocksdb_options_set_error_if_exists(options, o);
        }
        public static void leveldb_options_set_info_log(IntPtr /*Options*/ options, IntPtr /* Logger */ logger) {
           NativeRocksDB.rocksdb_options_set_info_log(options, logger);
        }
        public static void leveldb_options_set_paranoid_checks(IntPtr /*Options*/ options, [MarshalAs(UnmanagedType.U1)] bool o) {
           NativeRocksDB.rocksdb_options_set_paranoid_checks(options, o);
        }
        public static void leveldb_options_set_env(IntPtr /*Options*/ options, IntPtr /*Env*/ env) {
           NativeRocksDB.rocksdb_options_set_env(options, env);
        }
        public static void leveldb_options_set_write_buffer_size(IntPtr /*Options*/ options, UIntPtr size) {
           NativeRocksDB.rocksdb_options_set_write_buffer_size(options, size);
        }
        public static void leveldb_options_set_max_open_files(IntPtr /*Options*/ options, int max) {
           NativeRocksDB.rocksdb_options_set_max_open_files(options, max);
        }
        public static void leveldb_options_set_cache(IntPtr /*Options*/ options, IntPtr /*Cache*/ cache) {
           // ignore, as far as I could quickly tell there is no RocksDB equivalent that matches smoothly
        }
        public static void leveldb_options_set_block_size(IntPtr /*Options*/ options, UIntPtr size) {
           // ignore for now unless shown it's needed.
        }
        public static void leveldb_options_set_block_restart_interval(IntPtr /*Options*/ options, int interval) {
           // ignore for now unless shown it's needed.
        }
        public static void leveldb_options_set_compression(IntPtr /*Options*/ options, CompressionType level) {
           //NativeRocksDB.rocksdb_options_set_compression(options, level);
        }
        public static void leveldb_options_set_comparator(IntPtr /*Options*/ options, IntPtr /*Comparator*/ comparer) {
           NativeRocksDB.rocksdb_options_set_comparator(options, comparer);
        }
        public static void leveldb_options_set_filter_policy(IntPtr /*Options*/ options, IntPtr /*FilterPolicy*/ policy) {
           // ignore for now unless shown it's needed.
        }
        #endregion

        #region ReadOptions
        public static IntPtr leveldb_readoptions_create() {
            return NativeRocksDB.rocksdb_readoptions_create();
        }
        public static void leveldb_readoptions_destroy(IntPtr /*ReadOptions*/ options) {
            NativeRocksDB.rocksdb_readoptions_destroy(options);
        }
        public static void leveldb_readoptions_set_verify_checksums(IntPtr /*ReadOptions*/ options, [MarshalAs(UnmanagedType.U1)] bool o) {
            NativeRocksDB.rocksdb_readoptions_set_verify_checksums(options, o);
        }
        public static void leveldb_readoptions_set_fill_cache(IntPtr /*ReadOptions*/ options, [MarshalAs(UnmanagedType.U1)] bool o) {
            NativeRocksDB.rocksdb_readoptions_set_fill_cache(options, o);
        }
        public static void leveldb_readoptions_set_snapshot(IntPtr /*ReadOptions*/ options, IntPtr /*SnapShot*/ snapshot) {
            NativeRocksDB.rocksdb_readoptions_set_snapshot(options, snapshot);
        }
        #endregion

        #region WriteBatch
        public static IntPtr leveldb_writebatch_create() {
            return NativeRocksDB.rocksdb_writebatch_create();
        }
        public static void leveldb_writebatch_destroy(IntPtr /* WriteBatch */ batch) {
            NativeRocksDB.rocksdb_writebatch_destroy(batch);
        }
        public static void leveldb_writebatch_clear(IntPtr /* WriteBatch */ batch) {
            NativeRocksDB.rocksdb_writebatch_clear(batch);
        }
        public static void leveldb_writebatch_put(IntPtr /* WriteBatch */ batch, byte[] key, UIntPtr keylen, byte[] val, UIntPtr vallen) {
            NativeRocksDB.rocksdb_writebatch_put(batch, key, keylen, val, vallen);
        }
        public static void leveldb_writebatch_delete(IntPtr /* WriteBatch */ batch, byte[] key, UIntPtr keylen) {
            NativeRocksDB.rocksdb_writebatch_delete(batch, key, keylen);
        }
        public static void leveldb_writebatch_iterate(IntPtr /* WriteBatch */ batch, object state, Action<object, byte[], int, byte[], int> put, Action<object, byte[], int> deleted) {
            NativeRocksDB.rocksdb_writebatch_iterate(batch, state, put, deleted);
        }
        #endregion

        #region WriteOptions
        public static IntPtr leveldb_writeoptions_create() {
            return NativeRocksDB.rocksdb_writeoptions_create();
        }
        public static void leveldb_writeoptions_destroy(IntPtr /*WriteOptions*/ options) {
            NativeRocksDB.rocksdb_writeoptions_destroy(options);
        }
        public static void leveldb_writeoptions_set_sync(IntPtr /*WriteOptions*/ options, [MarshalAs(UnmanagedType.U1)] bool o) {
            NativeRocksDB.rocksdb_writeoptions_set_sync(options, o);
        }
        #endregion

        #region Cache
        public static IntPtr leveldb_cache_create_lru(int capacity) {
            return NativeRocksDB.rocksdb_cache_create_lru(capacity);
        }

        public static void leveldb_cache_destroy(IntPtr /*Cache*/ cache) {
            NativeRocksDB.rocksdb_cache_destroy(cache);
        }
        #endregion

        #region Comparator

        public static IntPtr /* leveldb_comparator_t* */
            leveldb_comparator_create(
            IntPtr /* void* */ state,
            IntPtr /* void (*)(void*) */ destructor,
            IntPtr
                /* int (*compare)(void*,
                                  const char* a, size_t alen,
                                  const char* b, size_t blen) */
                compare,
            IntPtr /* const char* (*)(void*) */ name) {
            return NativeRocksDB.rocksdb_comparator_create(state, destructor, compare, name);
        }

        public static void leveldb_comparator_destroy(IntPtr /* leveldb_comparator_t* */ cmp) {
            NativeRocksDB.rocksdb_comparator_destroy(cmp);
        }

        #endregion
    }

    public static class NativeHelper
    {
        public static void CheckError(IntPtr error)
        {
            if (error != IntPtr.Zero)
            {
                string message = Marshal.PtrToStringAnsi(error);
                Native.leveldb_free(error);
                throw new LevelDBException(message);
            }
        }
    }
}
