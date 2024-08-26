# Reliability Assessment - Monitoring Health
![](./img/well-architected-hub.png)
## Question: How do you plan to monitor health?

A reliable monitoring and alerting strategy can keep your operations teams informed of your environment's health status and ensure that you meet the established reliability targets for your workload.

### Comments


## Question Responses

### We can monitor each flow of our workload individually and assess its health state.
Your monitoring strategy targets each flow individually to assess its individual health state. You can assess your flow's health state based on your health model, which is driven by your committed service-level objectives (SLOs) and service-level agreements (SLAs).

### We ensure our team is familiar with telemetry at various levels.
Your team understands which metrics, logs, and traces are available across various levels, such as the platform, infrastructure, and application levels.

### We're intentional about our monitoring system design.
You're intentional about data sources and the retention time, so you can balance comprehensiveness of data with data sovereignty and cost efficiency.

### We send out alerts when the health of a flow degrades.
You send alerts to a defined set of operators when the health state of flows decreases. Operators know how to handle the incidents or where to find instructions. You also consider sending proactive notifications when health states improve.

### We present the workload and individual flow health in a simple visual to users who have the right permissions.
You present the health state of the workload and its individual flows by using dashboards that provide the appropriate insights for the individual user. An appropriate visual, for example a colored box or a traffic light, is prominently positioned on the dashboard to provide immediate insight into the current workload state: healthy, degraded, or unhealthy.

### We've taken measures to stay updated on the health of platforms that our workload depends on.
Incorporate cloud platform monitoring and alerting services, including platform-level health and resource-level health. To mitigate health impacts, the relevant mechanisms and processes utilize these services.