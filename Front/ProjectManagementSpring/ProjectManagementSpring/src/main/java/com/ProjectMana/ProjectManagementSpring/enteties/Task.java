package com.ProjectMana.ProjectManagementSpring.enteties;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Entity

@Data
@AllArgsConstructor
@NoArgsConstructor
public class Task {
    @Id
    @GeneratedValue
    private Integer no ;
    private String title ;
    private String description ;

    @Column(name = "endDate")
    private String end ;

  @Column(name = "startDate")
  private String start ;
    private Integer status ;

    @ManyToOne
    @JoinColumn(
            name = "project_id"
    )

    @JsonBackReference
    private project project  ;

    @ManyToOne
    @JoinColumn(
            name = "user_id"
    )
    @JsonBackReference
    private UserT user ;

  public Task(Integer no) {
    this.no = no;
  }
}
