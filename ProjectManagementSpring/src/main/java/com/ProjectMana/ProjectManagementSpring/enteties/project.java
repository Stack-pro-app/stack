package com.ProjectMana.ProjectManagementSpring.enteties;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import com.fasterxml.jackson.annotation.JsonBackReference;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;

@Entity

@Data
@AllArgsConstructor
@NoArgsConstructor
public class project {

    @Id
    @GeneratedValue
    private Integer id ;

    private String  projectName ;

    private String  projectDescrp;

    private String  start;
   @Column(name = "endDate")
    private String  end;

    private Integer  budget;

  private String clientName ;

  @ManyToOne
  @JoinColumn(name = "workspace_id")
  @JsonIgnore
  private WorkSpace workSpace;



  @OneToMany(mappedBy = "project", cascade = CascadeType.ALL)
  @JsonManagedReference

    public List<Task> tasks ;

    public project(Integer id) {
        this.id = id;
    }
}
