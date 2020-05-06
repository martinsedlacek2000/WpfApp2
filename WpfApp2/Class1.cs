//Oracle DAL

using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using VF.Practices.DataAccess;

namespace VF.DAL.DARS
{

	#region Schema

	public class TKBODSchema : VF.Practices.DataAccess.Schema
	{
		private static ArrayList _entries;
		public static SchemaItem FTKBIDENT = new SchemaItem("FTKBIDENT", System.Data.DbType.Decimal, true, false, false, true, false, false);
		public static SchemaItem FTKBMERENI = new SchemaItem("FTKBMERENI", System.Data.DbType.Decimal, false, false, false, false, true, false);
		public static SchemaItem FTKBBLENKER = new SchemaItem("FTKBBLENKER", System.Data.DbType.Decimal, false, false, false, false, false, false);
		public static SchemaItem FTKBPOZADOV = new SchemaItem("FTKBPOZADOV", System.Data.DbType.Decimal, false, true, false, false, false, false);
		public static SchemaItem FTKBZARIC = new SchemaItem("FTKBZARIC", System.Data.DbType.Decimal, false, true, false, false, true, false);
		public static SchemaItem FTKBVZDALENOST = new SchemaItem("FTKBVZDALENOST", System.Data.DbType.Decimal, false, true, false, false, false, false);
		public static SchemaItem FTKBDOBA = new SchemaItem("FTKBDOBA", System.Data.DbType.Decimal, false, true, false, false, false, false);
		public static SchemaItem FTKBHODNOTA = new SchemaItem("FTKBHODNOTA", System.Data.DbType.Decimal, false, true, false, false, false, false);
		public static SchemaItem FTKBHCHYBA = new SchemaItem("FTKBHCHYBA", System.Data.DbType.Decimal, false, true, false, false, false, false);
		public static SchemaItem FTKBVELICINA = new SchemaItem("FTKBVELICINA", System.Data.DbType.Decimal, false, true, false, false, true, false);
		public static SchemaItem FTKBGEOMETRIE = new SchemaItem("FTKBGEOMETRIE", System.Data.DbType.AnsiString, SchemaItemJustify.None, 63, true, false, false, false);
		public static SchemaItem FTKBPOPIS = new SchemaItem("FTKBPOPIS", System.Data.DbType.AnsiString, SchemaItemJustify.None, 63, true, false, false, false);

		public override ArrayList SchemaEntries
		{
			get
			{
				if (_entries == null)
				{
					_entries = new ArrayList();

					_entries.Add(TKBODSchema.FTKBIDENT);
					_entries.Add(TKBODSchema.FTKBMERENI);
					_entries.Add(TKBODSchema.FTKBBLENKER);
					_entries.Add(TKBODSchema.FTKBPOZADOV);
					_entries.Add(TKBODSchema.FTKBZARIC);
					_entries.Add(TKBODSchema.FTKBVZDALENOST);
					_entries.Add(TKBODSchema.FTKBDOBA);
					_entries.Add(TKBODSchema.FTKBHODNOTA);
					_entries.Add(TKBODSchema.FTKBHCHYBA);
					_entries.Add(TKBODSchema.FTKBVELICINA);
					_entries.Add(TKBODSchema.FTKBGEOMETRIE);
					_entries.Add(TKBODSchema.FTKBPOPIS);

					TKBODSchema.FTKBIDENT.Properties.Add("SEQ:I", "SEQ_TKBOD_ID");
				}
				return _entries;
			}
		}

		public static bool HasAutoKey
		{
			//TODO - check
			get { return true; }
		}

		public static bool HasRowID
		{
			//TODO - doesn't generated
			//README
			//A rowid is a pseudo column, that uniquely identifies a row within a table, but not within a database.
			//It is possible for two rows of two different tables stored in the same cluster to have the same rowid.
			//
			//Set by Database Type
			//         Oracle -> implicit true but can work with false
			//   MsSql, MySql -> false
			get { return false; }
		}
	}
	#endregion

	public abstract class _TKBOD : DBObject
	{
		#region C'tor
		public _TKBOD()
		{
			TKBODSchema _schema = new TKBODSchema();
			this.SchemaEntries = _schema.SchemaEntries;
			this.SchemaGlobal = "";
		}
		#endregion

		#region Data Manipulation Functions

		public override void FlushData()
		{
			this._whereClause = null;
			this._aggregateClause = null;
			base.FlushData();
		}

		/// <summary>
		/// Loads the business object with info from the database, based on the requested primary key.
		/// </summary>
		/// <param name="FTKBIDENT"></param>
		/// <returns>A Boolean indicating success or failure of the query</returns>
		public bool LoadByPrimaryKey(Decimal FTKBIDENT)
		{
			switch (this.DefaultCommandType)
			{
				case CommandType.StoredProcedure:
					ListDictionary parameters = new ListDictionary();

					// Add in parameters
					parameters.Add(TKBODSchema.FTKBIDENT.FieldName, FTKBIDENT);
					return base.LoadFromSql(this.SchemaStoredProcedureWithSeparator + "proc_TKBODLoadByPrimaryKey", parameters, CommandType.StoredProcedure);

				case CommandType.Text:
					this.Query.ClearAll();
					this.Where.WhereClauseReset();

					this.Where.FTKBIDENT.Value = FTKBIDENT;
					return this.Query.Load();

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}

		/// <summary>
		/// Loads all records from the table.
		/// </summary>
		/// <returns>A Boolean indicating success or failure of the query</returns>
		public bool LoadAll()
		{
			switch (this.DefaultCommandType)
			{
				case CommandType.StoredProcedure:
					return base.LoadFromSql(this.SchemaStoredProcedureWithSeparator + "proc_TKBODLoadAll", null, CommandType.StoredProcedure);

				case CommandType.Text:
					this.Query.ClearAll();
					this.Where.WhereClauseReset();
					return this.Query.Load();

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}

		/// <summary>
		/// Adds a new record to the internal table.
		/// </summary>
		public override void AddNew()
		{
			base.AddNew();
			this.ApplyDefaults();
		}

		/// <summary>
		/// Apply any default values to columns
		/// </summary>
		protected override void ApplyDefaults()
		{
		}

		#endregion

		#region Commands

		protected override DbCommand GetInsertCommand(CommandType commandType)
		{
			DbCommand dbCommand;

			// Create the Database object, using the default database service. The
			// default database service is determined through configuration.
			Database db = GetDatabase();

			switch (commandType)
			{
				case CommandType.StoredProcedure:
					string sqlCommand = this.SchemaStoredProcedureWithSeparator + "proc_TKBODInsert";
					dbCommand = db.GetStoredProcCommand(sqlCommand);
					db.AddParameter(dbCommand, "FTKBIDENT", System.Data.DbType.Decimal, 0, ParameterDirection.InputOutput, true, 0, 0, "FTKBIDENT", DataRowVersion.Default, Convert.DBNull);
					CreateParameters(db, dbCommand);

					return dbCommand;

				case CommandType.Text:
					this.Query.ClearAll();
					this.Where.WhereClauseReset();
					foreach (SchemaItem item in this.SchemaEntries)
					{
						if (!item.IsComputed)
						{
							if ((item.IsAutoKey && this.IdentityInsert) || !item.IsAutoKey)
							{
								this.Query.AddInsertColumn(item);
							}
						}
					}
					dbCommand = this.Query.GetInsertCommandWrapper();

					dbCommand.Parameters.Clear();
					if (this.IdentityInsert)
					{
						db.AddInParameter(dbCommand, "FTKBIDENT", System.Data.DbType.Decimal, "FTKBIDENT", DataRowVersion.Default);
					}
					else
					{
						db.AddParameter(dbCommand, "FTKBIDENT", System.Data.DbType.Decimal, 0, ParameterDirection.InputOutput, true, 0, 0, "FTKBIDENT", DataRowVersion.Default, Convert.DBNull);
					}
					CreateParameters(db, dbCommand);

					return dbCommand;

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}

		protected override DbCommand GetUpdateCommand(CommandType commandType)
		{
			DbCommand dbCommand;

			// Create the Database object, using the default database service. The
			// default database service is determined through configuration.
			Database db = GetDatabase();

			switch (commandType)
			{
				case CommandType.StoredProcedure:
					string sqlCommand = this.SchemaStoredProcedureWithSeparator + "proc_TKBODUpdate";
					dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddInParameter(dbCommand, "FTKBIDENT", System.Data.DbType.Decimal, "FTKBIDENT", DataRowVersion.Current);
					CreateParameters(db, dbCommand);

					return dbCommand;

				case CommandType.Text:
					this.Query.ClearAll();
					foreach (SchemaItem item in this.SchemaEntries)
					{
						if (!(item.IsAutoKey || item.IsComputed))
						{
							this.Query.AddUpdateColumn(item);
						}
					}

					this.Where.WhereClauseReset();
					this.Where.FTKBIDENT.Operator = WhereParameter.Operand.Equal;
					dbCommand = this.Query.GetUpdateCommandWrapper();

					dbCommand.Parameters.Clear();
					CreateParameters(db, dbCommand);
					db.AddInParameter(dbCommand, "FTKBIDENT", System.Data.DbType.Decimal, "FTKBIDENT", DataRowVersion.Current);
					return dbCommand;

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}

		protected override DbCommand GetDeleteCommand(CommandType commandType)
		{
			DbCommand dbCommand;

			// Create the Database object, using the default database service. The
			// default database service is determined through configuration.
			Database db = GetDatabase();

			switch (commandType)
			{
				case CommandType.StoredProcedure:
					string sqlCommand = this.SchemaStoredProcedureWithSeparator + "proc_TKBODDelete";
					dbCommand = db.GetStoredProcCommand(sqlCommand);
					db.AddInParameter(dbCommand, "FTKBIDENT", System.Data.DbType.Decimal, "FTKBIDENT", DataRowVersion.Current);
					return dbCommand;

				case CommandType.Text:
					this.Query.ClearAll();
					this.Where.WhereClauseReset();
					this.Where.FTKBIDENT.Operator = WhereParameter.Operand.Equal;
					dbCommand = this.Query.GetDeleteCommandWrapper();

					dbCommand.Parameters.Clear();
					db.AddInParameter(dbCommand, "FTKBIDENT", System.Data.DbType.Decimal, "FTKBIDENT", DataRowVersion.Current);
					return dbCommand;

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}

		private void CreateParameters(Database db, DbCommand dbCommand)
		{
			db.AddInParameter(dbCommand, "FTKBMERENI", System.Data.DbType.Decimal, "FTKBMERENI", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBBLENKER", System.Data.DbType.Decimal, "FTKBBLENKER", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBPOZADOV", System.Data.DbType.Decimal, "FTKBPOZADOV", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBZARIC", System.Data.DbType.Decimal, "FTKBZARIC", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBVZDALENOST", System.Data.DbType.Decimal, "FTKBVZDALENOST", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBDOBA", System.Data.DbType.Decimal, "FTKBDOBA", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBHODNOTA", System.Data.DbType.Decimal, "FTKBHODNOTA", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBHCHYBA", System.Data.DbType.Decimal, "FTKBHCHYBA", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBVELICINA", System.Data.DbType.Decimal, "FTKBVELICINA", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBGEOMETRIE", System.Data.DbType.AnsiString, "FTKBGEOMETRIE", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "FTKBPOPIS", System.Data.DbType.AnsiString, "FTKBPOPIS", DataRowVersion.Current);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Identifikátor měřicího bodu (sekvence)
		/// </summary>
		public virtual Decimal FTKBIDENT
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBIDENT.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBIDENT.FieldName, value);
			}
		}
		/// <summary>
		/// Příslušnost měřicího bodu ke konkrétnímu měření
		/// </summary>
		public virtual Decimal FTKBMERENI
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBMERENI.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBMERENI.FieldName, value);
			}
		}
		/// <summary>
		/// Příznak měření blenkeru
		/// </summary>
		public virtual Decimal FTKBBLENKER
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBBLENKER.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBBLENKER.FieldName, value);
			}
		}
		/// <summary>
		/// Požadovaná hodnota měřicího bodu
		/// </summary>
		public virtual Decimal FTKBPOZADOV
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBPOZADOV.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBPOZADOV.FieldName, value);
			}
		}
		/// <summary>
		/// Použitý zářič
		/// </summary>
		public virtual Decimal FTKBZARIC
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBZARIC.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBZARIC.FieldName, value);
			}
		}
		/// <summary>
		/// Vzdálenost přístroje od zdroje IZ
		/// </summary>
		public virtual Decimal FTKBVZDALENOST
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBVZDALENOST.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBVZDALENOST.FieldName, value);
			}
		}
		/// <summary>
		/// Doba ozařování [s]
		/// </summary>
		public virtual Decimal FTKBDOBA
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBDOBA.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBDOBA.FieldName, value);
			}
		}
		/// <summary>
		/// Skutečná hodnota měřicího bodu
		/// </summary>
		public virtual Decimal FTKBHODNOTA
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBHODNOTA.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBHODNOTA.FieldName, value);
			}
		}
		/// <summary>
		/// Nejistota hodnoty měřicího bodu
		/// </summary>
		public virtual Decimal FTKBHCHYBA
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBHCHYBA.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBHCHYBA.FieldName, value);
			}
		}
		/// <summary>
		/// Veličina hodnoty měřicího bodu
		/// </summary>
		public virtual Decimal FTKBVELICINA
		{
			get
			{
				return this.GetDecimal(TKBODSchema.FTKBVELICINA.FieldName);
			}
			set
			{
				this.SetDecimal(TKBODSchema.FTKBVELICINA.FieldName, value);
			}
		}
		/// <summary>
		/// Geometrie referenčního bodu měření
		/// </summary>
		[ColumnLength(63)]
		public virtual String FTKBGEOMETRIE
		{
			get
			{
				return this.GetString(TKBODSchema.FTKBGEOMETRIE.FieldName);
			}
			set
			{
				this.SetString(TKBODSchema.FTKBGEOMETRIE.FieldName, value);
			}
		}
		/// <summary>
		/// Poznámka k měřicímu bodu
		/// </summary>
		[ColumnLength(63)]
		public virtual String FTKBPOPIS
		{
			get
			{
				return this.GetString(TKBODSchema.FTKBPOPIS.FieldName);
			}
			set
			{
				this.SetString(TKBODSchema.FTKBPOPIS.FieldName, value);
			}
		}

		public override string TableName
		{
			get { return "TKBOD"; }
		}

		#endregion

		#region String Properties

		/// <summary>
		/// Identifikátor měřicího bodu (sekvence)
		/// </summary>
		public virtual string s_FTKBIDENT
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBIDENT.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBIDENT.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBIDENT.FieldName);
				else
					this.FTKBIDENT = base.SetDecimalAsString(TKBODSchema.FTKBIDENT.FieldName, value);
			}
		}
		/// <summary>
		/// Příslušnost měřicího bodu ke konkrétnímu měření
		/// </summary>
		public virtual string s_FTKBMERENI
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBMERENI.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBMERENI.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBMERENI.FieldName);
				else
					this.FTKBMERENI = base.SetDecimalAsString(TKBODSchema.FTKBMERENI.FieldName, value);
			}
		}
		/// <summary>
		/// Příznak měření blenkeru
		/// </summary>
		public virtual string s_FTKBBLENKER
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBBLENKER.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBBLENKER.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBBLENKER.FieldName);
				else
					this.FTKBBLENKER = base.SetDecimalAsString(TKBODSchema.FTKBBLENKER.FieldName, value);
			}
		}
		/// <summary>
		/// Požadovaná hodnota měřicího bodu
		/// </summary>
		public virtual string s_FTKBPOZADOV
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBPOZADOV.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBPOZADOV.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBPOZADOV.FieldName);
				else
					this.FTKBPOZADOV = base.SetDecimalAsString(TKBODSchema.FTKBPOZADOV.FieldName, value);
			}
		}
		/// <summary>
		/// Použitý zářič
		/// </summary>
		public virtual string s_FTKBZARIC
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBZARIC.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBZARIC.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBZARIC.FieldName);
				else
					this.FTKBZARIC = base.SetDecimalAsString(TKBODSchema.FTKBZARIC.FieldName, value);
			}
		}
		/// <summary>
		/// Vzdálenost přístroje od zdroje IZ
		/// </summary>
		public virtual string s_FTKBVZDALENOST
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBVZDALENOST.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBVZDALENOST.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBVZDALENOST.FieldName);
				else
					this.FTKBVZDALENOST = base.SetDecimalAsString(TKBODSchema.FTKBVZDALENOST.FieldName, value);
			}
		}
		/// <summary>
		/// Doba ozařování [s]
		/// </summary>
		public virtual string s_FTKBDOBA
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBDOBA.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBDOBA.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBDOBA.FieldName);
				else
					this.FTKBDOBA = base.SetDecimalAsString(TKBODSchema.FTKBDOBA.FieldName, value);
			}
		}
		/// <summary>
		/// Skutečná hodnota měřicího bodu
		/// </summary>
		public virtual string s_FTKBHODNOTA
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBHODNOTA.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBHODNOTA.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBHODNOTA.FieldName);
				else
					this.FTKBHODNOTA = base.SetDecimalAsString(TKBODSchema.FTKBHODNOTA.FieldName, value);
			}
		}
		/// <summary>
		/// Nejistota hodnoty měřicího bodu
		/// </summary>
		public virtual string s_FTKBHCHYBA
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBHCHYBA.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBHCHYBA.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBHCHYBA.FieldName);
				else
					this.FTKBHCHYBA = base.SetDecimalAsString(TKBODSchema.FTKBHCHYBA.FieldName, value);
			}
		}
		/// <summary>
		/// Veličina hodnoty měřicího bodu
		/// </summary>
		public virtual string s_FTKBVELICINA
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBVELICINA.FieldName) ? string.Empty : base.GetDecimalAsString(TKBODSchema.FTKBVELICINA.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBVELICINA.FieldName);
				else
					this.FTKBVELICINA = base.SetDecimalAsString(TKBODSchema.FTKBVELICINA.FieldName, value);
			}
		}
		/// <summary>
		/// Geometrie referenčního bodu měření
		/// </summary>
		[ColumnLength(63)]
		public virtual string s_FTKBGEOMETRIE
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBGEOMETRIE.FieldName) ? string.Empty : base.GetStringAsString(TKBODSchema.FTKBGEOMETRIE.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBGEOMETRIE.FieldName);
				else
					this.FTKBGEOMETRIE = base.SetStringAsString(TKBODSchema.FTKBGEOMETRIE.FieldName, value);
			}
		}
		/// <summary>
		/// Poznámka k měřicímu bodu
		/// </summary>
		[ColumnLength(63)]
		public virtual string s_FTKBPOPIS
		{
			get
			{
				return this.IsColumnNull(TKBODSchema.FTKBPOPIS.FieldName) ? string.Empty : base.GetStringAsString(TKBODSchema.FTKBPOPIS.FieldName);
			}
			set
			{
				if (string.Empty == value)
					this.SetColumnNull(TKBODSchema.FTKBPOPIS.FieldName);
				else
					this.FTKBPOPIS = base.SetStringAsString(TKBODSchema.FTKBPOPIS.FieldName, value);
			}
		}

		#endregion

		#region Where Clause
		public class WhereClause
		{
			public WhereClause(DBObject entity)
			{
				this._entity = entity;
			}

			public TearOffWhereParameter TearOff
			{
				get
				{
					if (_tearOff == null)
					{
						_tearOff = new TearOffWhereParameter(this);
					}

					return _tearOff;
				}
			}

			#region TearOff's
			public class TearOffWhereParameter
			{
				public TearOffWhereParameter(WhereClause clause)
				{
					this._clause = clause;
				}


				public WhereParameter FTKBIDENT
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBIDENT);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBMERENI
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBMERENI);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBBLENKER
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBBLENKER);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBPOZADOV
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBPOZADOV);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBZARIC
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBZARIC);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBVZDALENOST
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBVZDALENOST);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBDOBA
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBDOBA);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBHODNOTA
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBHODNOTA);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBHCHYBA
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBHCHYBA);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBVELICINA
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBVELICINA);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBGEOMETRIE
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBGEOMETRIE);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}
				public WhereParameter FTKBPOPIS
				{
					get
					{
						WhereParameter wp = new WhereParameter(TKBODSchema.FTKBPOPIS);
						this._clause._entity.Query.AddWhereParameter(wp);
						return wp;
					}
				}

				private WhereClause _clause;
			}
			#endregion

			public WhereParameter FTKBIDENT
			{
				get
				{
					if (_FTKBIDENT_W == null)
					{
						_FTKBIDENT_W = TearOff.FTKBIDENT;
					}
					return _FTKBIDENT_W;
				}
			}
			public WhereParameter FTKBMERENI
			{
				get
				{
					if (_FTKBMERENI_W == null)
					{
						_FTKBMERENI_W = TearOff.FTKBMERENI;
					}
					return _FTKBMERENI_W;
				}
			}
			public WhereParameter FTKBBLENKER
			{
				get
				{
					if (_FTKBBLENKER_W == null)
					{
						_FTKBBLENKER_W = TearOff.FTKBBLENKER;
					}
					return _FTKBBLENKER_W;
				}
			}
			public WhereParameter FTKBPOZADOV
			{
				get
				{
					if (_FTKBPOZADOV_W == null)
					{
						_FTKBPOZADOV_W = TearOff.FTKBPOZADOV;
					}
					return _FTKBPOZADOV_W;
				}
			}
			public WhereParameter FTKBZARIC
			{
				get
				{
					if (_FTKBZARIC_W == null)
					{
						_FTKBZARIC_W = TearOff.FTKBZARIC;
					}
					return _FTKBZARIC_W;
				}
			}
			public WhereParameter FTKBVZDALENOST
			{
				get
				{
					if (_FTKBVZDALENOST_W == null)
					{
						_FTKBVZDALENOST_W = TearOff.FTKBVZDALENOST;
					}
					return _FTKBVZDALENOST_W;
				}
			}
			public WhereParameter FTKBDOBA
			{
				get
				{
					if (_FTKBDOBA_W == null)
					{
						_FTKBDOBA_W = TearOff.FTKBDOBA;
					}
					return _FTKBDOBA_W;
				}
			}
			public WhereParameter FTKBHODNOTA
			{
				get
				{
					if (_FTKBHODNOTA_W == null)
					{
						_FTKBHODNOTA_W = TearOff.FTKBHODNOTA;
					}
					return _FTKBHODNOTA_W;
				}
			}
			public WhereParameter FTKBHCHYBA
			{
				get
				{
					if (_FTKBHCHYBA_W == null)
					{
						_FTKBHCHYBA_W = TearOff.FTKBHCHYBA;
					}
					return _FTKBHCHYBA_W;
				}
			}
			public WhereParameter FTKBVELICINA
			{
				get
				{
					if (_FTKBVELICINA_W == null)
					{
						_FTKBVELICINA_W = TearOff.FTKBVELICINA;
					}
					return _FTKBVELICINA_W;
				}
			}
			public WhereParameter FTKBGEOMETRIE
			{
				get
				{
					if (_FTKBGEOMETRIE_W == null)
					{
						_FTKBGEOMETRIE_W = TearOff.FTKBGEOMETRIE;
					}
					return _FTKBGEOMETRIE_W;
				}
			}
			public WhereParameter FTKBPOPIS
			{
				get
				{
					if (_FTKBPOPIS_W == null)
					{
						_FTKBPOPIS_W = TearOff.FTKBPOPIS;
					}
					return _FTKBPOPIS_W;
				}
			}

			private WhereParameter _FTKBIDENT_W = null;
			private WhereParameter _FTKBMERENI_W = null;
			private WhereParameter _FTKBBLENKER_W = null;
			private WhereParameter _FTKBPOZADOV_W = null;
			private WhereParameter _FTKBZARIC_W = null;
			private WhereParameter _FTKBVZDALENOST_W = null;
			private WhereParameter _FTKBDOBA_W = null;
			private WhereParameter _FTKBHODNOTA_W = null;
			private WhereParameter _FTKBHCHYBA_W = null;
			private WhereParameter _FTKBVELICINA_W = null;
			private WhereParameter _FTKBGEOMETRIE_W = null;
			private WhereParameter _FTKBPOPIS_W = null;

			public void WhereClauseReset()
			{
				_FTKBIDENT_W = null;
				_FTKBMERENI_W = null;
				_FTKBBLENKER_W = null;
				_FTKBPOZADOV_W = null;
				_FTKBZARIC_W = null;
				_FTKBVZDALENOST_W = null;
				_FTKBDOBA_W = null;
				_FTKBHODNOTA_W = null;
				_FTKBHCHYBA_W = null;
				_FTKBVELICINA_W = null;
				_FTKBGEOMETRIE_W = null;
				_FTKBPOPIS_W = null;

				this._entity.Query.FlushWhereParameters();

			}

			private DBObject _entity;
			private TearOffWhereParameter _tearOff;

		}

		public WhereClause Where
		{
			get
			{
				if (_whereClause == null)
				{
					_whereClause = new WhereClause(this);
				}

				return _whereClause;
			}
		}

		private WhereClause _whereClause = null;
		#endregion

		#region Aggregate Clause
		public class AggregateClause
		{
			public AggregateClause(DBObject entity)
			{
				this._entity = entity;
			}

			public TearOffAggregateParameter TearOff
			{
				get
				{
					if (_tearOff == null)
					{
						_tearOff = new TearOffAggregateParameter(this);
					}

					return _tearOff;
				}
			}

			#region TearOff's
			public class TearOffAggregateParameter
			{
				public TearOffAggregateParameter(AggregateClause clause)
				{
					this._clause = clause;
				}


				public AggregateParameter FTKBIDENT
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBIDENT);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBMERENI
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBMERENI);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBBLENKER
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBBLENKER);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBPOZADOV
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBPOZADOV);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBZARIC
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBZARIC);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBVZDALENOST
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBVZDALENOST);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBDOBA
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBDOBA);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBHODNOTA
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBHODNOTA);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBHCHYBA
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBHCHYBA);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBVELICINA
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBVELICINA);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBGEOMETRIE
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBGEOMETRIE);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}
				public AggregateParameter FTKBPOPIS
				{
					get
					{
						AggregateParameter ap = new AggregateParameter(TKBODSchema.FTKBPOPIS);
						this._clause._entity.Query.AddAggregateParameter(ap);
						return ap;
					}
				}

				private AggregateClause _clause;
			}
			#endregion
			public AggregateParameter FTKBIDENT
			{
				get
				{
					if (_FTKBIDENT_W == null)
					{
						_FTKBIDENT_W = TearOff.FTKBIDENT;
					}
					return _FTKBIDENT_W;
				}
			}
			public AggregateParameter FTKBMERENI
			{
				get
				{
					if (_FTKBMERENI_W == null)
					{
						_FTKBMERENI_W = TearOff.FTKBMERENI;
					}
					return _FTKBMERENI_W;
				}
			}
			public AggregateParameter FTKBBLENKER
			{
				get
				{
					if (_FTKBBLENKER_W == null)
					{
						_FTKBBLENKER_W = TearOff.FTKBBLENKER;
					}
					return _FTKBBLENKER_W;
				}
			}
			public AggregateParameter FTKBPOZADOV
			{
				get
				{
					if (_FTKBPOZADOV_W == null)
					{
						_FTKBPOZADOV_W = TearOff.FTKBPOZADOV;
					}
					return _FTKBPOZADOV_W;
				}
			}
			public AggregateParameter FTKBZARIC
			{
				get
				{
					if (_FTKBZARIC_W == null)
					{
						_FTKBZARIC_W = TearOff.FTKBZARIC;
					}
					return _FTKBZARIC_W;
				}
			}
			public AggregateParameter FTKBVZDALENOST
			{
				get
				{
					if (_FTKBVZDALENOST_W == null)
					{
						_FTKBVZDALENOST_W = TearOff.FTKBVZDALENOST;
					}
					return _FTKBVZDALENOST_W;
				}
			}
			public AggregateParameter FTKBDOBA
			{
				get
				{
					if (_FTKBDOBA_W == null)
					{
						_FTKBDOBA_W = TearOff.FTKBDOBA;
					}
					return _FTKBDOBA_W;
				}
			}
			public AggregateParameter FTKBHODNOTA
			{
				get
				{
					if (_FTKBHODNOTA_W == null)
					{
						_FTKBHODNOTA_W = TearOff.FTKBHODNOTA;
					}
					return _FTKBHODNOTA_W;
				}
			}
			public AggregateParameter FTKBHCHYBA
			{
				get
				{
					if (_FTKBHCHYBA_W == null)
					{
						_FTKBHCHYBA_W = TearOff.FTKBHCHYBA;
					}
					return _FTKBHCHYBA_W;
				}
			}
			public AggregateParameter FTKBVELICINA
			{
				get
				{
					if (_FTKBVELICINA_W == null)
					{
						_FTKBVELICINA_W = TearOff.FTKBVELICINA;
					}
					return _FTKBVELICINA_W;
				}
			}
			public AggregateParameter FTKBGEOMETRIE
			{
				get
				{
					if (_FTKBGEOMETRIE_W == null)
					{
						_FTKBGEOMETRIE_W = TearOff.FTKBGEOMETRIE;
					}
					return _FTKBGEOMETRIE_W;
				}
			}
			public AggregateParameter FTKBPOPIS
			{
				get
				{
					if (_FTKBPOPIS_W == null)
					{
						_FTKBPOPIS_W = TearOff.FTKBPOPIS;
					}
					return _FTKBPOPIS_W;
				}
			}

			private AggregateParameter _FTKBIDENT_W = null;
			private AggregateParameter _FTKBMERENI_W = null;
			private AggregateParameter _FTKBBLENKER_W = null;
			private AggregateParameter _FTKBPOZADOV_W = null;
			private AggregateParameter _FTKBZARIC_W = null;
			private AggregateParameter _FTKBVZDALENOST_W = null;
			private AggregateParameter _FTKBDOBA_W = null;
			private AggregateParameter _FTKBHODNOTA_W = null;
			private AggregateParameter _FTKBHCHYBA_W = null;
			private AggregateParameter _FTKBVELICINA_W = null;
			private AggregateParameter _FTKBGEOMETRIE_W = null;
			private AggregateParameter _FTKBPOPIS_W = null;

			public void AggregateClauseReset()
			{
				_FTKBIDENT_W = null;
				_FTKBMERENI_W = null;
				_FTKBBLENKER_W = null;
				_FTKBPOZADOV_W = null;
				_FTKBZARIC_W = null;
				_FTKBVZDALENOST_W = null;
				_FTKBDOBA_W = null;
				_FTKBHODNOTA_W = null;
				_FTKBHCHYBA_W = null;
				_FTKBVELICINA_W = null;
				_FTKBGEOMETRIE_W = null;
				_FTKBPOPIS_W = null;

				this._entity.Query.FlushAggregateParameters();

			}

			private DBObject _entity;
			private TearOffAggregateParameter _tearOff;

		}

		public AggregateClause Aggregate
		{
			get
			{
				if (_aggregateClause == null)
				{
					_aggregateClause = new AggregateClause(this);
				}

				return _aggregateClause;
			}
		}

		private AggregateClause _aggregateClause = null;
		#endregion
	}
}
