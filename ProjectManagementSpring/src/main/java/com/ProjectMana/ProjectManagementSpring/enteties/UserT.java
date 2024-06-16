package com.ProjectMana.ProjectManagementSpring.enteties;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import jakarta.persistence.*;

import lombok.AllArgsConstructor;
import lombok.NoArgsConstructor;

import java.util.List;

@Entity


@AllArgsConstructor
@NoArgsConstructor

public class UserT {
    @Id
    @GeneratedValue
    public Integer id ;

     public String userName ;

  @Column(unique = true)
  public String authId;

     public String email;





    @OneToMany(mappedBy = "user")
    @JsonManagedReference
     List<Task> tasks;

  public UserT(Integer id) {
    this.id = id;
  }

  public UserT(String authId) {
    this.authId = authId;
  }


  public List<Task> getTasks() {
    return tasks;
  }

  public void setTasks(List<Task> tasks) {
    this.tasks = tasks;
  }

  public Integer getId() {
    return id;
  }

  public void setId(Integer id) {
    this.id = id;
  }

  public String getUserName() {
    return userName;
  }

  public void setUserName(String userName) {
    this.userName = userName;
  }

  public String getAuthId() {
    return this.authId;
  }

  public void setAuthId(String authId) {
    this.authId = authId;
  }

  public String getEmail() {
    return email;
  }

  public void setEmail(String email) {
    this.email = email;
  }


}
