﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.SqlServer
{
    using System.Data.SqlClient;

    /// <summary>
    ///     Detects the exceptions caused by SQL Azure transient failures.
    /// </summary>
    internal static class SqlAzureRetriableExceptionDetector
    {
        public static bool ShouldRetryOn(Exception ex)
        {
            var sqlException = ex as SqlException;
            if (sqlException != null)
            {
                // Enumerate through all errors found in the exception.
                foreach (SqlError err in sqlException.Errors)
                {
                    switch (err.Number)
                    {
                            // SQL Error Code: 40627
                            // Operation on server YYYY and database XXXX is in progress.  Please wait a few minutes before trying again.
                        case 40627:
                            // SQL Error Code: 40613
                            // Database XXXX on server YYYY is not currently available. Please retry the connection later. If the problem persists, contact customer 
                            // support, and provide them the session tracing ID of ZZZZZ.
                        case 40613:
                            // SQL Error Code: 40501
                            // The service is currently busy. Retry the request after 10 seconds. Code: (reason code to be decoded).
                        case 40501:
                            // SQL Error Code: 40197
                            // The service has encountered an error processing your request. Please try again.
                        case 40197:
                            // SQL Error Code: 40143
                            // The service has encountered an error processing your request. Please try again.
                        case 40143:
                            // SQL Error Code: 10060
                            // A network-related or instance-specific error occurred while establishing a connection to SQL Server. 
                            // The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server 
                            // is configured to allow remote connections. (provider: TCP Provider, error: 0 - A connection attempt failed 
                            // because the connected party did not properly respond after a period of time, or established connection failed 
                            // because connected host has failed to respond.)"}
                        case 10060:
                            // SQL Error Code: 10054
                            // A transport-level error has occurred when sending the request to the server. 
                            // (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by the remote host.)
                        case 10054:
                            // SQL Error Code: 10053
                            // A transport-level error has occurred when receiving results from the server.
                            // An established connection was aborted by the software in your host machine.
                        case 10053:
                            // SQL Error Code: 233
                            // The client was unable to establish a connection because of an error during connection initialization process before login. 
                            // Possible causes include the following: the client tried to connect to an unsupported version of SQL Server; the server was too busy 
                            // to accept new connections; or there was a resource limitation (insufficient memory or maximum allowed connections) on the server. 
                            // (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by the remote host.)
                        case 233:
                            // SQL Error Code: 64
                            // A connection was successfully established with the server, but then an error occurred during the login process. 
                            // (provider: TCP Provider, error: 0 - The specified network name is no longer available.) 
                        case 64:
                            // DBNETLIB Error Code: 20
                            // The instance of SQL Server you attempted to connect to does not support encryption.
                        case 20:
                            return true;
                            // This exception can be thrown even if the operation completed succesfully, so it's safer to let the application fail.
                            // DBNETLIB Error Code: -2
                            // Timeout expired. The timeout period elapsed prior to completion of the operation or the server is not responding. The statement has been terminated. 
                            //case -2:
                    }
                }

                return false;
            }

            if (ex is TimeoutException)
            {
                return true;
            }

            return false;
        }
    }
}
