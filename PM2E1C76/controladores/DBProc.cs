using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM2E1C76.controladores
{
    public class DBProc
    {
        readonly SQLiteAsyncConnection _connection;
        public DBProc() { }

        public DBProc(string dbpath)
        {
            _connection = new SQLiteAsyncConnection(dbpath);

            _connection.CreateTableAsync<Lugar>().Wait();
        }

        public Task<int> AggLugar(Lugar lugares)
        {
            if(lugares.Id == 0)
            {
                return _connection.InsertAsync(lugares);
            }
            else
            {
                return _connection.UpdateAsync(lugares);
            }
        }

        public Task<List<Lugar>> GetAllLugares()
        {
            return _connection.Table<Lugar>().ToListAsync();
        }

        public Task<Lugar> GetById(int id)
        {
            return _connection.Table <Lugar>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> EliminarLugar(Lugar lugares)
        {
            return _connection.DeleteAsync(lugares);
        }
    }
}
