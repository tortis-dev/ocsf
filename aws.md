# AWS Security Lake

To ingest your application’s OCSF events into **AWS Security Lake**, follow these high-level steps:
### 1. **Format OCSF Events as Per Security Lake Requirements**
- **OCSF (Open Cybersecurity Schema Framework)** events should be serialized as JSON.
- Make sure each event aligns with the OCSF schema (including required fields).

### 2. **Select a Data Ingestion Method**
AWS Security Lake can ingest log data via **Amazon S3 buckets** or direct **AWS integrations**. The typical flow for custom applications is:
- **Write OCSF events to an S3 bucket** registered with Security Lake.

#### **A. Direct to S3 Bucket (Recommended for Most Apps)**
1. **Create or identify an S3 bucket** in your AWS account.
  - You can use an existing bucket or a new one.

2. **Register the S3 bucket as a custom source in AWS Security Lake.**
  - Go to the AWS Console → Security Lake → Sources → Add custom source.
  - Enter the S3 bucket details and set the log type (specify OCSF type).

3. **From your application**, upload OCSF event files (JSON) to the `prefix`/`folders` you specified in the registration step.
  - Each file can contain one or multiple events.
  - Names like `event_20240605_0001.json` are typical.

#### Example S3 upload code in C#:
``` csharp
using Amazon.S3;
using Amazon.S3.Transfer;

// eventJson: your OCSF event as JSON string
public async Task UploadEventAsync(string eventJson, string bucketName, string key)
{
    using var client = new AmazonS3Client();
    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(eventJson));
    
    var uploadRequest = new TransferUtilityUploadRequest
    {
        InputStream = stream,
        Key = key, // e.g. "ocsf/events/2024/06/05/event_1.json"
        BucketName = bucketName,
        ContentType = "application/json"
    };
    var transferUtility = new TransferUtility(client);
    await transferUtility.UploadAsync(uploadRequest);
}
```
### 3. **Security Lake Discovery & Ingestion**
- Security Lake automatically discovers new files in the registered S3 buckets and ingests them.
- Events become available for search and analytics in Lake Formation–compatible tools (e.g., Amazon Athena).

### 4. **Optional: Use AWS APIs/SDKs/Partner Integrations**
If your volume is high or you want automation:
- Consider batching OCSF events and uploading hourly/daily for best performance.
- Refer to [AWS Security Lake documentation](https://docs.aws.amazon.com/security-lake/latest/userguide/) for integration patterns.

## **Summary Checklist**
1. Format events as OCSF JSON.
2. Write/batch them as files to a registered S3 bucket.
3. Register the bucket as a custom source in Security Lake (specify correct log type/path).
4. Security Lake will take care of ingestion.

## **Official AWS Documentation**
- [AWS Security Lake Custom Source Integration](https://docs.aws.amazon.com/security-lake/latest/userguide/custom-sources.html)
- [OCSF Schema](https://schema.ocsf.io/)
- [Integrating Custom Sources](https://docs.aws.amazon.com/security-lake/latest/userguide/register-custom-source.html)

**Tip:** If you want to automate or scale up, consider using AWS Lambda or Kinesis Firehose as bridges to S3.
