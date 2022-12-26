package shop;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.Objects;

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
                ", categoryId='" + categoryId + '\'' +
                ", title='" + title + '\'' +
                ", alias='" + alias + '\'' +
                ", content='" + content + '\'' +
                ", price='" + price + '\'' +
                ", oldPrice='" + oldPrice + '\'' +
                ", status='" + status + '\'' +
                ", keywords='" + keywords + '\'' +
                ", description='" + description + '\'' +
                ", hit='" + hit + '\'' +
                ", img='" + img + '\'' +
                ", cat='" + cat + '\'' +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        ProductData that = (ProductData) o;
        return Objects.equals(id, that.id) && Objects.equals(categoryId, that.categoryId) && Objects.equals(title, that.title) && Objects.equals(alias, that.alias) && Objects.equals(content, that.content) && Objects.equals(price, that.price) && Objects.equals(oldPrice, that.oldPrice) && Objects.equals(status, that.status) && Objects.equals(keywords, that.keywords) && Objects.equals(description, that.description) && Objects.equals(hit, that.hit) && Objects.equals(img, that.img) && Objects.equals(cat, that.cat);
    }

    public boolean contentEquals(ProductData that) {
        if (this == that) return true;
        return Objects.equals(title, that.title) && Objects.equals(content, that.content) && Objects.equals(price, that.price) && Objects.equals(oldPrice, that.oldPrice) && Objects.equals(status, that.status) && Objects.equals(keywords, that.keywords) && Objects.equals(description, that.description) && Objects.equals(hit, that.hit);
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, categoryId, title, alias, content, price, oldPrice, status, keywords, description, hit, img, cat);
    }

    // конструктор с другим объектом в качестве параметра
    // конструктор с json файла
}

