package shop;

import com.fasterxml.jackson.annotation.JsonProperty;

public class ProductData {
    public String id;
    @JsonProperty("category_id")
    public String categoryId;
    public String title;
    public String alias;
    public String content;
    public String price;
    @JsonProperty("old_price")
    public String oldPrice;
    public String status;
    public String keywords;
    public String description;
    public String hit;
    public String img;
    public String cat;

    public ProductData() {
    }

    public ProductData(String id, String categoryId, String title, String alias,
                       String content, String price, String oldPrice, String status, String keywords,
                       String description, String hit) {
        this.id = id;
        this.categoryId = categoryId;
        this.title = title;
        this.alias = alias;
        this.content = content;
        this.price = price;
        this.oldPrice = oldPrice;
        this.status = status;
        this.keywords = keywords;
        this.description = description;
        this.hit = hit;
    }

    @Override
    public String toString() {
        return "ProductData{" +
                "id='" + id + '\'' +
                ", title='" + title + '\'' +
                ", price='" + price + '\'' +
                '}';
    }
// конструктор с другим объектом в качестве параметра
    // конструктор с json файла
}

