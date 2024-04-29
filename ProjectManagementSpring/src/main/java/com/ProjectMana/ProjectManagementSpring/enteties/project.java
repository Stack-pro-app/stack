package com.ProjectMana.ProjectManagementSpring.enteties;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

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



}
