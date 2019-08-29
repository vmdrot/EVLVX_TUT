#ifndef _RSLT_CODES_PUB_H_
#define _RSLT_CODES_PUB_H_

#define RSLT_BASE_ADO 0x100  /* shift for ADO result codes */

typedef enum rslt_code_t {
    RSLT_SUCCESS,      
    RSLT_NOT_SUCCESS,                   
    RSLT_MEMORY_ALLOCATION_FAILURE,     
    RSLT_RESOURCE_FAILURE,              
    RSLT_INVALID_PARAMETER,             
    RSLT_INVALID_CALL,             
    RSLT_OBJECT_NOT_FOUND,              
    RSLT_OBJECT_EXISTS,                 
    RSLT_ACCESS_DENIED,                 
    RSLT_NOT_ALLOWED,                   
    RSLT_UNKNOWN_OS_ERROR,                                      
    RSLT_ALREADY_INITIALIZED,           /**< module is already initialized */
    RSLT_NOT_INITIALIZED,               /**< module is not initialized */
    RSLT_FUNCTION_NOT_IMPLEMENTED,      /**< function not implemented yet */
    RSLT_CMD_TIMEOUT,                   /**< Timeout waiting on command response */
    RSLT_PROPERTY_NOT_FOUND,            /**< Property not found */
    RSLT_INVALID_PROPERTY,              /**< Property id or type is not valid */
    RSLT_SERVICE_NOT_AVAILABLE,         /**< Service is not running */
    RSLT_NULL_FIELD,                    /**< NULL field value (e.g. for MsiRecordGetInteger) */
    RSLT_NO_MORE_DATA,                  /**< No more data to continue enumeration */

    RSLT_COM_OPERATION_FAILED,
    RSLT_WMI_OPERATION_FAILED,
    RSLT_ADSI_OPERATION_FAILED,
    RSLT_CONNECTION_FAILED,

    RSLT_ADO_INVALID_PATH_DB_SCRIPT  = RSLT_BASE_ADO,   /**< invalid path to db script */
    RSLT_ADO_OPEN_DB_SCRIPT,                            /**< cant't open db script */
    RSLT_ADO_PARSE_DB_SCRIPT,                           /**< can't parse db script */
    RSLT_ADO_CREATE_DB,                                 /**< can't create db */
    RSLT_ADO_DROP_DB,                                   /**< can't drop db */
    RSLT_ADO_OPEN_CONNECTION,                           /**< can't create db connection */
    RSLT_ADO_CLOSE_CONNECTION,                          /**< can't close db connection */
    RSLT_ADO_INITIALIZE_COMMAND,                        /**< can't initialize ado_command */
    RSLT_ADO_RELEASE_COMMAND,                           /**< can't release ado_command */
    RSLT_ADO_EXECUTE_QUERY,                             /**< can't execute query */
    RSLT_ADO_TIMEOUT,                                   /**< timeout expired */
    RSLT_ADO_DB_ALREADY_EXIST,                          /**< cannot create db because it already exist */
    RSLT_ADO_SET_PARAM_FAIL,                             /**< failed to set a parameter for a parameterized query */

} rslt_code_t;

#endif                 /* _RSLT_CODES_PUB_H_ */
