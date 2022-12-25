package shop;

import com.fasterxml.jackson.annotation.JsonFormat;

public class ApiResponse {

    public int id;
    @JsonFormat(shape = JsonFormat.Shape.NUMBER)
    public boolean status;

    public ApiResponse() {
    }

    public ApiResponse(int id, boolean status) {
        this.id = id;
        this.status = status;
    }

    @Override
    public String toString() {
        return "ApiResponse{" +
                "id=" + id +
                ", status=" + status +
                '}';
    }
}
