# Security Assessment - Data Classification
## Question: Have you classified all data persisted by the workload?

Apply and maintain meaningful classification and sensitivity labeling on all workload data and systems that are involved in data access and handling.

### Comments


## Question Responses

### [X] **We performed an inventory showing where all data is stored in our workload.**
To design a secure system, you need to know where all the data is. Otherwise, you might end up with a compromise solution that doesn't meet your security needs. This situation can lead to either overprotection of data, which wastes resources and affects performance, or under protection of data, which exposes it to increased risks.
#### Comments



#### References


### [X] **We aligned the taxonomy for our data classifications with our business requirements.**
Taxonomy is a hierarchical representation of classification that has named entities to indicate categorization criteria. Designing a standardized taxonomy facilitates consistency and reliability when protecting data. The taxonomy should align with the types of data that your business defines important or needs to protect.

### [X] **We designed our architecture according to the classification labels.**
The categorization should factor into the architectural decisions. The most obvious area is your segmentation strategy, which should consider the varied classification labels. For example, the labels should influence the traffic isolation boundaries.

### [X] **We regularly review the classification and taxonomy applied to our workload.**
The data that applications use can change over time as they develop and improve. You should check your classification system often to make sure that it covers any new types of data in the application.

### [X] **Where available, we apply consistent classification tagging that's designed for querying.**
Whether you have an existing system or are building a new one, when you apply labels, you should maintain consistency in the key/value pair. Standardization through schema helps ensure that reporting is accurate. It also minimizes the chance of variation and avoids creation of custom queries. Build automated checks to catch invalid entries.

