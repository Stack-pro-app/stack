package com.ProjectMana.ProjectManagementSpring.enteties;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Entity

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class WorkSpace {

  @Id
  @GeneratedValue
  private Integer id ;
  private String Name ;




  @ManyToOne
  @JoinColumn(name = "adminAuthId", referencedColumnName = "authId")
  private UserT admin;

 @ElementCollection
  List<String> users ;

  @OneToMany(mappedBy = "workSpace", cascade = CascadeType.ALL)
  private List<project> projects;

}
