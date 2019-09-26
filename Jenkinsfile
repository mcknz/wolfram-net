pipeline {
  agent any
  tools {
    maven 'Maven-3.6.0'
    jdk 'OpenJDK-11'
  }
  stages {
    stage ('Checkout code') {
      steps {
        git credentialsId: 'mcknz-ssh', url: 'git@github.com:mcknz/wolfram-java.git'
      }
    }
    stage ('Run Tests') {
      steps {
        sh '''mvn \
            -q \
            -DdriverType=${driverType} \
            -DpageTimeout=${pageTimeout} \
            -DdriverPathAndName=${driverPathAndName} \
          test''' 
      }
    }
    stage ('Generate Reports') {
      steps {
        cucumber buildStatus: 'UNSTABLE',
                 fileIncludePattern: '**/*.json',
                 trendsLimit: 10,
                 classifications: [
                   [
                     'key': 'Browser',
                     'value': 'Chrome'
                   ]
                 ]
      }
    }
  }
}
