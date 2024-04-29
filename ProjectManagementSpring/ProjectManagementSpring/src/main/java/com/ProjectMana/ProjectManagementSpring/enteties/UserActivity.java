package com.ProjectMana.ProjectManagementSpring.enteties;

import com.fasterxml.jackson.annotation.JsonBackReference;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor

public class UserActivity {
  @Id
  @GeneratedValue
  private Integer id;

  private LocalDateTime date;
  private Integer status ;

  @ManyToOne
  @JoinColumn(name = "user_id")

  private UserT user;

  @ManyToOne
  @JoinColumn(name = "task_id")

  private Task task;



}
