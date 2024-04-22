void setBuildStatus(String message, String state) {
  step([
      $class: "GitHubCommitStatusSetter",
      reposSource: [$class: "ManuallyEnteredRepositorySource", url: "https://github.com/PBSW/DLS-Compulsory-Assignment-2"],
      contextSource: [$class: "ManuallyEnteredCommitContextSource", context: "ci/jenkins/build-status"],
      errorHandlers: [[$class: "ChangingBuildStatusErrorHandler", result: "UNSTABLE"]],
      statusResultSource: [ $class: "ConditionalStatusResultSource", results: [[$class: "AnyBuildResult", message: message, state: state]] ]
  ]);
}

pipeline {
    agent any
    stages {
        stage('Unit testing') {
            steps {
                sh 'dotnet test Backend/PS-Tests'
                sh 'dotnet test Backend/MS-Tests'
            }
        }
        stage('Build docker images') {
            steps {
                sh 'docker compose build'
            }
        }
        stage('Publish docker images') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'Dockerhub', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) 
                {
                    sh 'docker login -u $DOCKER_USER -p $DOCKER_PASS'
                    sh 'docker compose push'
                }
            }
        }
        stage('Deploy - fake') {
            steps {
                echo 'serve application'
            }
        }
        post {
            success {
                setBuildStatus("Build succeeded", "SUCCESS");
            }
            failure {
                setBuildStatus("Build failed", "FAILURE");
            }
        }
    }
}
