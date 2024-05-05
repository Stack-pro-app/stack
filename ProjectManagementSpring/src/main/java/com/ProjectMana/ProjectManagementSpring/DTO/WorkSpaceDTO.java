package com.ProjectMana.ProjectManagementSpring.DTO;

import com.ProjectMana.ProjectManagementSpring.enteties.UserT;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

@Data
@NoArgsConstructor
public class WorkSpaceDTO {
  private Integer id ;
  private String Name ;
  private String admin ;
  private List<String> users;

  public WorkSpaceDTO(Integer id, String name, String admin, List<String> users) {
    this.id = id;
    Name = name;
    this.admin = admin;
    this.users = users;
  }
}
