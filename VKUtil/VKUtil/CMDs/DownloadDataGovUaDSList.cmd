@echo off
REM INPUT_URLS_FILE: file with urls to be downloaded
SET INPUT_URLS_FILE=D:\home\vmdrot\DEV\EVLVX_TUT\VKUtil\VKUtil\ReadDataGovUaDSList.PassportXmlUrls.txt

REM SAVE_TO_DIR: path to directory where to save the downloaded files to
SET SAVE_TO_DIR=D:\home\vmdrot\Testing\OpenData\Input\Data.gov.ua\DataSets

REM QU_PARAM_4_FILE_NAME: name of a url's query string parameter 
REM to use a file name to be saved
SET QU_PARAM_4_FILE_NAME=dataset-id

REM SLEEP_BTW_REQUESTS_MS: interval to sleep (ms) between normal requests 
REM (experiment with these depending on the site's policy - 
REM some sites may limit frequency of requests to prohibit bots 
REM and make sure it's humans who interact with them
SET SLEEP_BTW_REQUESTS_MS=1325

REM SLEEP_BTW_ERRORS_MS: time to wait (ms) when an error is encountered 
REM (may be useful if some sites automatically block access for a 
REM given constant timeout)
SET SLEEP_BTW_ERRORS_MS=60000

REM MAX_RETRY_COUNT_ON_ERR: attempt to re-try download if it fails (for every url in the list)
SET MAX_RETRY_COUNT_ON_ERR=3

REM QUIT_ON_HTTP_ERROR_STATUSES: some sites a limiting a number of requests per client 
REM (e.g. per day) - useless to try to continue downloading once you've reached this limit;
REM if you know the http status(-es) the server returns to tell you that, you can specify these.
REM Can be a single status (e.g. 403 - Forbidden), or a list of statuses, comma-separated - e.g. '403,404,500,500.1'
SET QUIT_ON_HTTP_ERROR_STATUSES=403

REM bin\Debug\Evolvex.VKUtil.exe DownloadFiles D:\home\vmdrot\DEV\EVLVX_TUT\VKUtil\VKUtil\ReadDataGovUaDSList.PassportXmlUrls.txt D:\home\vmdrot\Testing\OpenData\Input\Data.gov.ua\DataSets "dataset-id" 200 10000 3
REM bin\Debug\Evolvex.VKUtil.exe DownloadFiles D:\home\vmdrot\DEV\EVLVX_TUT\VKUtil\VKUtil\ReadDataGovUaDSList.PassportXmlUrls.txt D:\home\vmdrot\Testing\OpenData\Input\Data.gov.ua\DataSets "dataset-id" 200 10000 3 403
bin\Debug\Evolvex.VKUtil.exe DownloadFiles "%INPUT_URLS_FILE%" "%SAVE_TO_DIR%" "%QU_PARAM_4_FILE_NAME%"  "%SLEEP_BTW_REQUESTS_MS%"  "%SLEEP_BTW_ERRORS_MS%" "%MAX_RETRY_COUNT_ON_ERR%" "%QUIT_ON_HTTP_ERROR_STATUSES%"
EXIT 0