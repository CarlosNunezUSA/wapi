' ' |
' ' |============================================================================================================
' ' |  Project:           DAL
' ' |------------------------------------------------------------------------------------------------------------
' ' |  
' ' |  Last modified by:  Carlos I. Nunez (carlos@compexc.com)
' ' |
' ' |  On date:           9/27/2014
' ' |
' ' |------------------------------------------------------------------------------------------------------------
' ' |  (c) 2014 Carlos I. Nunez, Miami - FL. All rights reserved.      
' ' |============================================================================================================
' ' |
Imports System.Data.Common

Namespace Data

    Public Class DataFacade

        Public Shared Function GetDataTableSP(factory As DatabaseFactory, stored_procedure As String, params As List(Of DbParameter)) As DataTable

            Using connection As DbConnection = factory.Factory.CreateConnection()

                connection.ConnectionString = factory.ConnectionString

                Using command As DbCommand = connection.CreateCommand()
                    command.CommandText = stored_procedure
                    command.CommandType = CommandType.StoredProcedure
                    If params IsNot Nothing Then
                        For Each p As DbParameter In params
                            command.Parameters.Add(p)
                        Next
                    End If
                    connection.Open()
                    Dim reader As DbDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)

                    If reader IsNot Nothing AndAlso reader.HasRows Then
                        Dim dt As New DataTable
                        dt.Load(reader)
                        Return dt
                    End If

                End Using

            End Using

            Return Nothing

        End Function

        Public Shared Function GetDataTable(factory As DatabaseFactory, query As String) As DataTable

            Using connection As DbConnection = factory.Factory.CreateConnection()

                connection.ConnectionString = factory.ConnectionString

                Using command As DbCommand = connection.CreateCommand()
                    command.CommandText = query
                    command.CommandType = CommandType.Text

                    connection.Open()
                    Dim reader As DbDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)

                    If reader IsNot Nothing AndAlso reader.HasRows Then
                        Dim dt As New DataTable
                        dt.Load(reader)
                        Return dt
                    End If

                End Using

            End Using

            Return Nothing

        End Function

        Private Shared Function GetSchemaFields(SchemaTable As DataTable) As List(Of SchemaField)

            Dim result As New List(Of SchemaField)

            For i As Integer = 0 To SchemaTable.Rows.Count - 1
                Dim qf As New SchemaField
                Dim properties As String = String.Empty
                For j = 0 To SchemaTable.Columns.Count - 1
                    properties += String.Format("{0}{1}{2}", SchemaTable.Columns(j).ColumnName, Chr(254), SchemaTable.Rows(i).Item(j).ToString)
                    If j < SchemaTable.Columns.Count - 1 Then properties += Chr(255)

                    If (IsDBNull(SchemaTable.Rows(i).Item(j)) = False) Then
                        Select Case SchemaTable.Columns(j).ColumnName
                            Case "ColumnName"
                                qf.ColumnName = SchemaTable.Rows(i).Item(j)
                            Case "ColumnOrdinal"
                                qf.ColumnOrdinal = SchemaTable.Rows(i).Item(j)
                            Case "ColumnSize"
                                If SchemaTable.Rows(i).Item(j) >= 214000 Then
                                    qf.ColumnSize = "MAX"
                                Else
                                    qf.ColumnSize = SchemaTable.Rows(i).Item(j)
                                End If
                            Case "NumericPrecision"
                                qf.NumericPrecision = SchemaTable.Rows(i).Item(j)
                            Case "NumericScale"
                                qf.NumericScale = SchemaTable.Rows(i).Item(j)
                            Case "IsUnique"
                                qf.IsUnique = SchemaTable.Rows(i).Item(j)
                            Case "BaseColumnName"
                                qf.BaseColumnName = SchemaTable.Rows(i).Item(j)
                            Case "BaseTableName"
                                qf.BaseTableName = SchemaTable.Rows(i).Item(j)
                            Case "DataType"
                                qf.DataType = CType(SchemaTable.Rows(i).Item(j), System.Type).FullName
                            Case "AllowDBNull"
                                qf.AllowDBNull = SchemaTable.Rows(i).Item(j)
                            Case "ProviderType"
                                qf.ProviderType = SchemaTable.Rows(i).Item(j)
                            Case "IsIdentity"
                                qf.IsIdentity = SchemaTable.Rows(i).Item(j)
                            Case "IsAutoIncrement"
                                qf.IsAutoIncrement = SchemaTable.Rows(i).Item(j)
                            Case "IsRowVersion"
                                qf.IsRowVersion = SchemaTable.Rows(i).Item(j)
                            Case "IsLong"
                                qf.IsLong = SchemaTable.Rows(i).Item(j)
                            Case "IsReadOnly"
                                qf.IsReadOnly = SchemaTable.Rows(i).Item(j)
                            Case "ProviderSpecificDataType"
                                qf.ProviderSpecificDataType = CType(SchemaTable.Rows(i).Item(j), System.Type).FullName
                            Case "DataTypeName"
                                qf.DataTypeName = SchemaTable.Rows(i).Item(j)
                            Case "UdtAssemblyQualifiedName"
                                qf.UdtAssemblyQualifiedName = SchemaTable.Rows(i).Item(j)
                            Case "IsColumnSet"
                                qf.IsColumnSet = SchemaTable.Rows(i).Item(j)
                            Case "NonVersionedProviderType"
                                qf.NonVersionedProviderType = SchemaTable.Rows(i).Item(j)
                            Case Else
                        End Select
                    End If
                Next
                qf.RawProperties = properties
                result.Add(qf)
            Next

            Return result

        End Function

        Public Shared Function ExecuteSP(factory As DatabaseFactory, stored_procedure As String, params As List(Of DbParameter)) As Integer

            Using connection As DbConnection = factory.Factory.CreateConnection()

                connection.ConnectionString = factory.ConnectionString

                Using command As DbCommand = connection.CreateCommand()
                    command.CommandText = stored_procedure
                    command.CommandType = CommandType.StoredProcedure
                    If params IsNot Nothing Then
                        For Each p As DbParameter In params
                            command.Parameters.Add(p)
                        Next
                    End If
                    connection.Open()

                    Return command.ExecuteNonQuery()
                End Using

            End Using

            Return -1

        End Function

        Public Shared Function ExecuteScalarSP(factory As DatabaseFactory, stored_procedure As String, params As List(Of DbParameter), output_param_name As String) As Object

            Using connection As DbConnection = factory.Factory.CreateConnection()

                connection.ConnectionString = factory.ConnectionString

                Using command As DbCommand = connection.CreateCommand()
                    command.CommandText = stored_procedure
                    command.CommandType = CommandType.StoredProcedure
                    If params IsNot Nothing Then
                        For Each p As DbParameter In params
                            command.Parameters.Add(p)
                        Next
                    End If
                    connection.Open()

                    command.ExecuteScalar()

                    Return command.Parameters(output_param_name).Value

                End Using

            End Using

            Return -1

        End Function

        Public Shared Function Execute(factory As DatabaseFactory, query As String) As Integer

            Using connection As DbConnection = factory.Factory.CreateConnection()

                connection.ConnectionString = factory.ConnectionString

                Using command As DbCommand = connection.CreateCommand()
                    command.CommandText = query
                    command.CommandType = CommandType.Text
                    connection.Open()
                    Return command.ExecuteNonQuery()
                End Using

            End Using

            Return -1

        End Function

        Public Shared Function GetSchemaDatatables(factory As DatabaseFactory) As List(Of SchemaTable)


            Dim result As New List(Of SchemaTable)
            Dim st As SchemaTable

            Dim connection As DbConnection = factory.Factory.CreateConnection
            connection.ConnectionString = factory.ConnectionString
            Dim command As DbCommand = Nothing

            Using connection

                'Connect to the database then retrieve the schema information.
                connection.Open()
                Dim tables As DataTable = connection.GetSchema("Tables")

                If Not tables Is Nothing AndAlso tables.Rows.Count > 0 Then

                    result = New List(Of SchemaTable)

                    For Each r As DataRow In tables.Rows

                        ' do not include views or dtproperties table
                        If r("TABLE_TYPE") = "VIEW" Then Continue For
                        If r("TABLE_NAME") = "dtproperties" Then Continue For

                        st = New SchemaTable
                        st.TableName = r("TABLE_NAME")
                        Using command
                            command = connection.CreateCommand()
                            command.CommandText = String.Format("SELECT * FROM [{0}]", st.TableName)
                            command.CommandType = CommandType.Text
                            Dim reader As DbDataReader = command.ExecuteReader()
                            If reader IsNot Nothing AndAlso reader.HasRows Then
                                st.Fields = GetSchemaFields(reader.GetSchemaTable)
                            Else
                                st.Fields = Nothing
                            End If
                            reader.Close()
                            result.Add(st)
                        End Using

                    Next

                End If

            End Using

            Return result

        End Function

    End Class

End Namespace