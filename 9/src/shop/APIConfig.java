package shop;

import com.fasterxml.jackson.annotation.JsonProperty;

public class APIConfig {
    @JsonProperty("BASE_URI")
    public String baseUri;
    @JsonProperty("BASE_PATH")
    public String basePath;
}
